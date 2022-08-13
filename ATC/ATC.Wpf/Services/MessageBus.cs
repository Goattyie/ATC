using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.Services
{
    internal interface IMessage { }
    internal class MessageSubscriber : IDisposable
    {
        public MessageSubscriber(Type subscriberType, Type msgType, Action<MessageSubscriber> disposeAction)
        {
            SubscriberType = subscriberType;
            MsgType = msgType;
            DisposeAction = disposeAction;
        }

        public Type SubscriberType { get; set; }
        public Type MsgType { get; set; }
        public Action<MessageSubscriber> DisposeAction { get; set; }

        public void Dispose()
        {
            DisposeAction?.Invoke(this);
        }
    }

    internal class MessageBus
    {
        private readonly ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> _container;

        public MessageBus()
        {
            _container = new ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>>();
        }

        public IDisposable Recieve<TMsg>(object subscriber, Func<TMsg, Task> hanlder) where TMsg : IMessage
        {
            var sub = new MessageSubscriber(subscriber.GetType(), typeof(TMsg), s => _container.TryRemove(s, out var _));

            _container.TryAdd(sub, @event => hanlder((TMsg)@event));

            return sub;
        }

        public async Task SendTo<TReciever>(IMessage msg)
        {
            var subType = typeof(TReciever);
            var msgType = msg.GetType();

            var tasks = _container.Where(x => x.Key.MsgType == msgType && x.Key.SubscriberType == subType)
                .Select(x => x.Value(msg));

            await Task.WhenAll(tasks);
        }
    }
}

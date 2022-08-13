using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.Services
{
    internal interface IEvent { }

    internal class EventBus
    {
        private ConcurrentDictionary<Type, Func<IEvent, Task>> _container;

        public EventBus()
        {
            _container = new ConcurrentDictionary<Type, Func<IEvent, Task>>();
        }

        public void Subscribe<TEvent>(Func<TEvent, Task> handle) where TEvent : IEvent
        {
            _container.TryAdd(typeof(TEvent), @event => handle((TEvent)@event));         
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent  
        { 
            var tasks = _container.Where(x => x.Key == typeof(TEvent))
                .Select(x => x.Value(@event));

            await Task.WhenAll(tasks);
        }
    }
}

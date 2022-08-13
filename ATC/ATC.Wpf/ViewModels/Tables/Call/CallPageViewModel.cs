using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Call;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Call
{
    internal class CallModelMessage : IMessage
    {
        public CallModel Call { get; set; }
    }

    internal class CallNewModelEvent : IEvent
    {
        public CallModel Call { get; set; }
    }

    internal class CallUpdateModelEvent : IEvent
    {
        public CallModel Call { get; set; }
    }

    internal class CallPageViewModel
    {
        private readonly ICallRepository _repository;
        private readonly MessageBus _messageBus;

        public ObservableCollection<CallModel> Calls { get; }
        public CallModel SelectedCall { get; set; }

        public CallPageViewModel(ICallRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Calls = new ObservableCollection<CallModel>();

            LoadData();

            eventBus.Subscribe<CallNewModelEvent>((@event) => { Calls.Add(@event.Call); return Task.CompletedTask; });
            eventBus.Subscribe<CallUpdateModelEvent>((@event) =>
            {
                var oldValue = Calls.First(x => x.Id == @event.Call.Id);
                var index = Calls.IndexOf(oldValue);

                Calls.Remove(oldValue);
                Calls.Insert(index, @event.Call);

                return Task.CompletedTask;
            });
        }

        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateCallWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedCall is null)
                return;

            new UpdateCallWindow().Show();

            await _messageBus.SendTo<UpdateCallWindowViewModel>(new CallModelMessage { Call = SelectedCall });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedCall != null)
            {
                await _repository.Delete(SelectedCall.Id);
                Calls.Remove(SelectedCall);
            }
        });

        private async Task LoadData()
        {
            Calls.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Calls.Add(item);
        }
    }
}

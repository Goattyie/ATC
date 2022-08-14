using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Abonent;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Abonent
{
    internal class AbonentModelMessage : IMessage
    {
        public AbonentModel Abonent { get; set; }
    }

    internal class AbonentNewModelEvent : IEvent
    {
        public AbonentModel Abonent { get; set; }
    }

    internal class AbonentUpdateModelEvent : IEvent
    {
        public AbonentModel Abonent { get; set; }
    }

    internal class AbonentPageViewModel : BindableBase
    {
        private readonly IAbonentRepository _repository;
        private readonly MessageBus _messageBus;

        public AbonentPageViewModel(IAbonentRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Abonents = new ObservableCollection<AbonentModel>();

            LoadData();

            eventBus.Subscribe<AbonentNewModelEvent>((@event) => { Abonents.Add(@event.Abonent); return Task.CompletedTask; });
            eventBus.Subscribe<AbonentUpdateModelEvent>((@event) =>
            {
                var oldValue = Abonents.First(x => x.Id == @event.Abonent.Id);
                var index = Abonents.IndexOf(oldValue);

                Abonents.Remove(oldValue);
                Abonents.Insert(index, @event.Abonent);

                return Task.CompletedTask;
            });
        }

        public ObservableCollection<AbonentModel> Abonents { get; set; }
        public AbonentModel SelectedAbonent { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateAbonentWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedAbonent is null)
                return;

            new UpdateAbonentWindow().Show();

            await _messageBus.SendTo<UpdateAbonentWindowViewModel>(new AbonentModelMessage { Abonent = SelectedAbonent });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedAbonent != null)
            {
                await _repository.Delete(SelectedAbonent.Id);
                Abonents.Remove(SelectedAbonent);
            }
        });

        private async Task LoadData()
        {
            Abonents.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Abonents.Add(item);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Tariff;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Tariff
{
    internal class TariffModelMessage : IMessage
    {
        public TariffModel Tariff { get; set; }
    }

    internal class TariffNewModelEvent : IEvent
    {
        public TariffModel Tariff { get; set; }
    }

    internal class TariffUpdateModelEvent : IEvent
    {
        public TariffModel Tariff { get; set; }
    }

    internal class TariffPageViewModel : BindableBase
    {
        private readonly ITariffRepository _repository;
        private readonly MessageBus _messageBus;

        public ObservableCollection<TariffModel> Tariffs { get; }
        public TariffModel SelectedTariff { get; set; }

        public TariffPageViewModel(ITariffRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Tariffs = new ObservableCollection<TariffModel>();

            LoadData();

            eventBus.Subscribe<TariffNewModelEvent>((@event) => { Tariffs.Add(@event.Tariff); return Task.CompletedTask; });
            eventBus.Subscribe<TariffUpdateModelEvent>((@event) =>
            {
                var oldValue = Tariffs.First(x => x.Id == @event.Tariff.Id);
                var index = Tariffs.IndexOf(oldValue);

                Tariffs.Remove(oldValue);
                Tariffs.Insert(index, @event.Tariff);

                return Task.CompletedTask;
            });
        }

        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateTariffWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedTariff is null)
                return;

            new UpdateTariffWindow().Show();

            await _messageBus.SendTo<UpdateTariffWindowViewModel>(new TariffModelMessage { Tariff = SelectedTariff });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedTariff != null)
            {
                await _repository.Delete(SelectedTariff.Id);
                Tariffs.Remove(SelectedTariff);
            }
        });

        private async Task LoadData()
        {
            Tariffs.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Tariffs.Add(item);
        }
    }
}

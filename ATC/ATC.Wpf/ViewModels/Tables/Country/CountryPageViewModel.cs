using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Country;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Country
{
    internal class CountryModelMessage : IMessage
    {
        public CountryModel Country { get; set; }
    }

    internal class CountryNewModelEvent : IEvent
    {
        public CountryModel Country { get; set; }
    }

    internal class CountryUpdateModelEvent : IEvent
    {
        public CountryModel Country { get; set; }
    }

    internal class CountryPageViewModel : BindableBase
    {
        private readonly ICountryRepository _repository;
        private readonly MessageBus _messageBus;

        public ObservableCollection<CountryModel> Countries { get; }
        public CountryModel SelectedCountry { get; set; }

        public CountryPageViewModel(ICountryRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Countries = new ObservableCollection<CountryModel>();

            LoadData();

            eventBus.Subscribe<CountryNewModelEvent>((@event) => { Countries.Add(@event.Country); return Task.CompletedTask; });
            eventBus.Subscribe<CountryUpdateModelEvent>((@event) =>
            {
                var oldValue = Countries.First(x => x.Id == @event.Country.Id);
                var index = Countries.IndexOf(oldValue);

                Countries.Remove(oldValue);
                Countries.Insert(index, @event.Country);

                return Task.CompletedTask;
            });
        }

        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateCountryWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedCountry is null)
                return;

            new UpdateCountryWindow().Show();

            await _messageBus.SendTo<UpdateCountryWindowViewModel>(new CountryModelMessage { Country = SelectedCountry });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedCountry != null)
            {
                await _repository.Delete(SelectedCountry.Id);
                Countries.Remove(SelectedCountry);
            }
        });

        private async Task LoadData()
        {
            Countries.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Countries.Add(item);
        }
    }
}

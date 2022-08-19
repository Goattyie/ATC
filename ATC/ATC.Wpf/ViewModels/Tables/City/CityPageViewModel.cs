using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.City;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.City
{
    internal class CityModelMessage : IMessage
    {
        public CityModel City { get; set; }
    }

    internal class CityNewModelEvent : IEvent
    {
        public CityModel City { get; set; }
    }

    internal class CityUpdateModelEvent : IEvent
    {
        public CityModel City { get; set; }
    }

    internal class CityPageViewModel : BindableBase
    {
        private readonly ICityRepository _repository;
        private readonly MessageBus _messageBus;

        public ObservableCollection<CityModel> Cities { get; }
        public CityModel SelectedCity { get; set; }

        public CityPageViewModel(ICityRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Cities = new ObservableCollection<CityModel>();

            LoadData();

            eventBus.Subscribe<CityNewModelEvent>((@event) => { Cities.Add(@event.City); return Task.CompletedTask; });
            eventBus.Subscribe<CityUpdateModelEvent>((@event) =>
            {
                var oldValue = Cities.First(x => x.Id == @event.City.Id);
                var index = Cities.IndexOf(oldValue);

                Cities.Remove(oldValue);
                Cities.Insert(index, @event.City);

                return Task.CompletedTask;
            });
        }

        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateCityWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedCity is null)
                return;

            new UpdateCityWindow().Show();

            await _messageBus.SendTo<UpdateCityWindowViewModel>(new CityModelMessage { City = SelectedCity });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            try
            {
                while (SelectedCity != null)
                {
                    await _repository.Delete(SelectedCity.Id);
                    Cities.Remove(SelectedCity);
                }
            }catch(Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _repository.DisposeAsync();
            }
        });

        private async Task LoadData()
        {
            Cities.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Cities.Add(item);
        }
    }
}

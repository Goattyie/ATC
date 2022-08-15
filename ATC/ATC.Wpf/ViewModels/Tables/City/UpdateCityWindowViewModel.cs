using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.City
{
    internal class UpdateCityWindowViewModel : BindableBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly EventBus _eventBus;

        public UpdateCityWindowViewModel(ICountryRepository countryRepository,
            ICityRepository cityRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _eventBus = eventBus;

            City = new();
            Countries = new();

            messageBus.Recieve<CityModelMessage>(this, LoadData);
        }

        public CityModel City { get; set; }
        public ObservableCollection<CountryModel> Countries { get; set; }
        public CountryModel SelectedCountry { get; set; }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            City.CountryId = SelectedCountry?.Id ?? 0;
            City.CountryName = SelectedCountry?.Name;

            try
            {
                await _cityRepository.Update(City);
                await _eventBus.Publish(new CityUpdateModelEvent { City = City });
                window.Close();
                MessageBoxManager.ShowInformation("Город успешно обновлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _countryRepository.DisposeAsync();
            }
        });

        private async Task LoadData(CityModelMessage msg)
        {
            City = msg.City;

            Countries.Clear();

            var data = await _countryRepository.Get();

            foreach (var item in data)
                Countries.Add(item);

            SelectedCountry = Countries.FirstOrDefault(x => x.Id == City.CountryId);
        }
    }
}

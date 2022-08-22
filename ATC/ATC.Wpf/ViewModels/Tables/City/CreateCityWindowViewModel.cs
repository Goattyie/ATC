using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.City
{
    internal class CreateCityWindowViewModel : BindableBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly EventBus _eventBus;

        public CreateCityWindowViewModel(ICountryRepository countryRepository,
            ICityRepository cityRepository,
            EventBus eventBus)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _eventBus = eventBus;

            City = new();
            Countries = new();

            LoadData();
        }

        public CityModel City { get; set; }
        public ObservableCollection<CountryModel> Countries { get; set; }
        public CountryModel SelectedCountry { get; set; }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            City.CountryId = SelectedCountry?.Id ?? 0;
            City.CountryName = SelectedCountry?.Name;

            try
            {
                await _cityRepository.Create(City);
                await _eventBus.Publish(new AbstractNewModelEvent<CityModel> { Model = City });
                window.Close();
                MessageBoxManager.ShowInformation("Город успешно добавлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _countryRepository.DisposeAsync();
            }
        });

        private async Task LoadData()
        {
            Countries.Clear();

            var data = await _countryRepository.Get();

            foreach (var item in data)
                Countries.Add(item);

            SelectedCountry = Countries.FirstOrDefault();
        }
    }
}

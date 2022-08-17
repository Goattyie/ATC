using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Area
{
    internal class CreateAreaWindowViewModel : BindableBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ICityRepository _cityRepository;
        private readonly EventBus _eventBus;

        public CreateAreaWindowViewModel(IAreaRepository areaRepository,
            ICityRepository cityRepository,
            EventBus eventBus)
        {
            _areaRepository = areaRepository;
            _cityRepository = cityRepository;
            _eventBus = eventBus;

            Area = new();
            Cities = new();

            LoadData();
        }

        public AreaModel Area { get; set; }
        public ObservableCollection<CityModel> Cities { get; set; }
        public CityModel SelectedCity { get; set; }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Area.CityId = SelectedCity?.Id ?? 0;
            Area.CityName = SelectedCity?.Name;

            try
            {
                await _areaRepository.Create(Area);
                await _eventBus.Publish(new AreaNewModelEvent { Area = Area });
                window.Close();
                MessageBoxManager.ShowInformation("Район успешно добавлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _areaRepository.DisposeAsync();
            }
        });

        private async Task LoadData()
        {
            Cities.Clear();

            var data = await _cityRepository.Get();

            foreach (var item in data)
                Cities.Add(item);

            SelectedCity = Cities.FirstOrDefault();
        }
    }
}

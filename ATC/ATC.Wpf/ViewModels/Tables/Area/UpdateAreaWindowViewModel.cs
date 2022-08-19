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

namespace ATC.Wpf.ViewModels.Tables.Area
{
    internal class UpdateAreaWindowViewModel : BindableBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ICityRepository _cityRepository;
        private readonly EventBus _eventBus;

        public UpdateAreaWindowViewModel(IAreaRepository areaRepository,
            ICityRepository cityRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _areaRepository = areaRepository;
            _cityRepository = cityRepository;
            _eventBus = eventBus;

            Area = new();
            Cities = new();

            messageBus.Recieve<AreaModelMessage>(this, LoadData);
        }

        public AreaModel Area { get; set; }
        public ObservableCollection<CityModel> Cities { get; set; }
        public CityModel SelectedCity { get; set; }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Area.CityId = SelectedCity?.Id ?? 0;
            Area.CityName = SelectedCity?.Name;

            try
            {
                await _areaRepository.Update(Area);
                await _eventBus.Publish(new AreaUpdateModelEvent { Area = Area });
                window.Close();
                MessageBoxManager.ShowInformation("Район успешно обновлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _areaRepository.DisposeAsync();
            }
        });

        private async Task LoadData(AreaModelMessage msg)
        {
            Area = new AreaModel 
            { 
                Id = msg.Area.Id, 
                Name = msg.Area.Name, 
                CityId = msg.Area.CityId 
            };

            Cities.Clear();

            var data = await _cityRepository.Get();

            foreach (var item in data)
                Cities.Add(item);

            SelectedCity = Cities.FirstOrDefault(x => x.Id == Area.CityId);
        }
    }
}

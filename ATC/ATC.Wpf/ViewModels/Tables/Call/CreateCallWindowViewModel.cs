using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Call
{
    internal class CreateCallWindowViewModel : BindableBase
    {
        private readonly ICallRepository _callRepository;
        private readonly IAtcRepository _atcRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAbonentRepository _abonentRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly EventBus _eventBus;

        public CreateCallWindowViewModel(ICallRepository callRepository, 
            IAtcRepository atcRepository, 
            ICityRepository cityRepository, 
            IAbonentRepository abonentRepository, 
            ITariffRepository tariffRepository,
            EventBus eventBus)
        {
            _callRepository = callRepository;
            _atcRepository = atcRepository;
            _cityRepository = cityRepository;
            _abonentRepository = abonentRepository;
            _tariffRepository = tariffRepository;
            _eventBus = eventBus;

            Call = new() { CallDate = DateTime.Now };

            Atces = new();
            Cities = new();
            Abonents = new();
            Tariffs = new();

            LoadData();
        }

        public CallModel Call { get; set; }
        public ObservableCollection<AtcModel> Atces { get; set; }
        public AtcModel SelectedAtc { get; set; }
        public ObservableCollection<CityModel> Cities { get; set; }
        public CityModel SelectedCity { get; set; }
        public ObservableCollection<AbonentModel> Abonents { get; set; }
        public AbonentModel SelectedAbonent { get; set; }
        public ObservableCollection<TariffModel> Tariffs { get; set; }
        public TariffModel SelectedTariff { get; set; }
        
        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Call.AtcId = SelectedAtc?.Id ?? 0;
            Call.AtcName = SelectedAtc?.Name;
            Call.AbonentId = SelectedAbonent?.Id ?? 0;
            Call.AbonentPhone = SelectedAbonent.Phone;
            Call.TariffId = SelectedTariff?.Id ?? 0;
            Call.TariffName = SelectedTariff?.Name;
            Call.CityId = SelectedCity?.Id ?? 0;
            Call.CityName = SelectedCity?.Name;

            try
            {
                await _callRepository.Create(Call);
                await _eventBus.Publish(new CallNewModelEvent { Call = Call });
                window.Close();
                MessageBoxManager.ShowInformation("Вызов успешно добавлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _callRepository.DisposeAsync();
            }
        });

        private async Task LoadData()
        {
            Atces.Clear();
            Cities.Clear();
            Abonents.Clear();
            Tariffs.Clear();

            var atces = await _atcRepository.Get();
            var cities = await _cityRepository.Get();
            var abonents = await _abonentRepository.Get();
            var tariffs = await _tariffRepository.Get();

            foreach (var item in atces)
                Atces.Add(item);

            foreach (var item in cities)
                Cities.Add(item);

            foreach (var item in abonents)
                Abonents.Add(item);

            foreach (var item in tariffs)
                Tariffs.Add(item);

            SelectedAtc = Atces.FirstOrDefault();
            SelectedCity = Cities.FirstOrDefault();
            SelectedAbonent = Abonents.FirstOrDefault();
            SelectedTariff = Tariffs.FirstOrDefault();
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Atc
{
    internal class CreateAtcWindowViewModel : BindableBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAtcRepository _atcRepository;
        private readonly EventBus _eventBus;

        public CreateAtcWindowViewModel(IAtcRepository atcRepository, IAreaRepository areaRepository, EventBus eventBus)
        {
            _areaRepository = areaRepository;
            _atcRepository = atcRepository;
            _eventBus = eventBus;

            Atc = new();
            Areas = new();

            LoadAreas();
        }

        public AtcModel Atc { get; set; }
        public ObservableCollection<AreaModel> Areas { get; set; }
        public AreaModel SelectedArea { get; set; }

        private async Task LoadAreas()
        {
            Areas.Clear();

            var data = await _areaRepository.Get();

            foreach (var node in data)
                Areas.Add(node);

            SelectedArea = Areas.FirstOrDefault();
        }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Atc.AreaId = SelectedArea?.Id ?? 0;
            Atc.AreaName = SelectedArea?.Name;

            try
            {
                await _atcRepository.Create(Atc);
                await _eventBus.Publish(new AtcNewModelEvent { Atc = Atc });
                window.Close();
                MessageBoxManager.ShowInformation("АТС успешно добавлена.");
               
            }
            catch (Exception ex) 
            { 
                MessageBoxManager.ShowError(ex.Message);
                await _atcRepository.DisposeAsync();
            }
        });
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Atc
{
    internal class UpdateAtcWindowViewModel : BindableBase
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAtcRepository _atcRepository;
        private readonly EventBus _eventBus;

        public UpdateAtcWindowViewModel(IAtcRepository atcRepository, IAreaRepository areaRepository, MessageBus messageBus, EventBus eventBus)
        {
            _areaRepository = areaRepository;
            _atcRepository = atcRepository;
            _eventBus = eventBus;

            Atc = new();
            Areas = new();

            messageBus.Recieve<AbstractModelMessage<AtcModel>>(this, LoadAtcModel);
        }

        public Action OnClose { get; set; }
        public AtcModel Atc { get; set; }
        public ObservableCollection<AreaModel> Areas { get; set; }
        public AreaModel SelectedArea { get; set; }

        private async Task LoadAtcModel(AbstractModelMessage<AtcModel> msg)
        {
            await LoadAreas();

            Atc = msg.Model;
            SelectedArea = Areas.FirstOrDefault(x => x.Id == Atc.AreaId);
        }

        private async Task LoadAreas()
        {
            Areas.Clear();

            var data = await _areaRepository.Get();

            foreach (var node in data)
                Areas.Add(node);
        }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Atc.AreaId = SelectedArea?.Id ?? 0;
            Atc.AreaName = SelectedArea?.Name;

            try
            {
                await _atcRepository.Update(Atc);
                await _eventBus.Publish(new AbstractUpdateModelEvent<AtcModel> { Model = Atc });
                window.Close();
                MessageBoxManager.ShowInformation("АТС успешно обновлена.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _atcRepository.DisposeAsync();
            }
        });
    }
}

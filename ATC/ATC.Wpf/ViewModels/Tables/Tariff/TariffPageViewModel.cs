using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Tariff;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Wpf.ViewModels.Tables.Tariff
{
    internal class TariffPageViewModel : AbstractTablePageViewModel<CreateTariffWindow, UpdateTariffWindow, TariffModel, UpdateTariffWindowViewModel>
    {
        public TariffPageViewModel(ITariffRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Название",
            "Дата начала",
            "Дата конца",
            "Коэффициент"
        };

        protected override IEnumerable<TariffModel> FilterData(IEnumerable<TariffModel> allData) => SelectedFilter switch
        {
            "Название" => allData.Where(x => x.Name.Contains(FilterValue)),
            "Дата начала" => allData.Where(x => x.StartDate.ToString("dd.MM.yyyy").Contains(FilterValue)),
            "Дата конца" => allData.Where(x => x.EndDate.ToString("dd.MM.yyyy").Contains(FilterValue)),
            "Коэффициент" => allData.Where(x => x.Ratio.ToString().Contains(FilterValue)),
            _ => allData
        };
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Atc;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Atc
{
    internal class AtcPageViewModel : AbstractTablePageViewModel<CreateAtcWindow, UpdateAtcWindow, AtcModel, UpdateAtcWindowViewModel>
    {
        public AtcPageViewModel(IAtcRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Название",
            "Район",
            "Адрес",
            "Код",
            "Год открытия",
        };

        protected override IEnumerable<AtcModel> FilterData(IEnumerable<AtcModel> allData) => SelectedFilter switch
        {
            "Название" => allData.Where(x => x.Name.Contains(FilterValue)),
            "Район" => allData.Where(x => x.AreaName.Contains(FilterValue)),
            "Адрес" => allData.Where(x => x.Address.Contains(FilterValue)),
            "Код" => allData.Where(x => x.Code.Contains(FilterValue)),
            "Год открытия" => allData.Where(x => x.OpenYear.ToString().Contains(FilterValue))
        };
    }
}

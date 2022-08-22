using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Area;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Wpf.ViewModels.Tables.Area
{
    internal class AreaPageViewModel : AbstractTablePageViewModel<CreateAreaWindow, UpdateAreaWindow, AreaModel, UpdateAreaWindowViewModel>
    {
        public AreaPageViewModel(IAreaRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string> { "Название", "Город" };

        protected override IEnumerable<AreaModel> FilterData(IEnumerable<AreaModel> allData) => SelectedFilter switch
        {
            "Название" => allData.Where(x => x.Name.Contains(FilterValue)),
            "Город" => allData.Where(x => x.CityName.Contains(FilterValue))
        };
    }
}

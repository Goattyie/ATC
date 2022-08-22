using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.City;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Wpf.ViewModels.Tables.City
{
    internal class CityPageViewModel : AbstractTablePageViewModel<CreateCityWindow, UpdateCityWindow, CityModel, UpdateCityWindowViewModel>
    {
        public CityPageViewModel(ICityRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string> { "Название", "Страна" };

        protected override IEnumerable<CityModel> FilterData(IEnumerable<CityModel> data) => SelectedFilter switch
        {
            "Название" => data.Where(x => x.Name.Contains(FilterValue)),
            "Страна" => data.Where(x => x.CountryName.Contains(FilterValue)),
            _ => data
        };
    }
}

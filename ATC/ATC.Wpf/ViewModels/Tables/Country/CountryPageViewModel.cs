using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Country;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Country
{
    internal class CountryPageViewModel : AbstractTablePageViewModel<CreateCountryWindow, UpdateCountryWindow, CountryModel, UpdateCountryWindowViewModel>
    {
        public CountryPageViewModel(ICountryRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string> { "Название" };
        protected override IEnumerable<CountryModel> FilterData(IEnumerable<CountryModel> data) => SelectedFilter switch
        {
            "Название" => data.Where(x => x.Name.Contains(FilterValue)),
            _ => data
        };
    }
}

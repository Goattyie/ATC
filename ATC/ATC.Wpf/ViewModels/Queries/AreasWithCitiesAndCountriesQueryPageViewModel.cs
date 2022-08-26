using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AreasWithCitiesAndCountriesQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AreasWithCitiesAndCountriesResult>
    {
        public AreasWithCitiesAndCountriesQueryPageViewModel(IAreasWithCitiesAndCountriesQuery query) : base(query)
        {
        }
    }
}

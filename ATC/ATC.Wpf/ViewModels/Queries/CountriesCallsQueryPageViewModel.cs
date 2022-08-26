using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CountriesCallsQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, CallsResult>
    {
        public CountriesCallsQueryPageViewModel(ICountriesCallsQuery query) : base(query)
        {
        }
    }
}

using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CitiesCallsQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, CallsResult>
    {
        public CitiesCallsQueryPageViewModel(ICitiesCallsQuery query) : base(query)
        {
        }
    }
}

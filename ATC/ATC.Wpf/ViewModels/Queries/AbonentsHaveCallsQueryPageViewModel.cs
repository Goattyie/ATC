using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsHaveCallsQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AbonentHaveCallsResult>
    {
        public AbonentsHaveCallsQueryPageViewModel(IAbonentsHaveCallsQuery query) : base(query)
        {
        }
    }
}

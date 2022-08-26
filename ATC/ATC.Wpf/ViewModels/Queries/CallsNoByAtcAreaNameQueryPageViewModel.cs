using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CallsNoByAtcAreaNameQueryPageViewModel : AbstractQueryPageViewModel<StringInput, CallsByAtcAreaNameResult>
    {
        public CallsNoByAtcAreaNameQueryPageViewModel(ICallsNoByAtcAreaNameQuery query) : base(query)
        {
        }
    }
}

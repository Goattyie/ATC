using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CallsByAtcAreaNameQueryPageViewModel : AbstractQueryPageViewModel<StringInput, CallsByAtcAreaNameResult>
    {
        public CallsByAtcAreaNameQueryPageViewModel(ICallsByAtcAreaNameQuery query) : base(query)
        {
        }
    }
}

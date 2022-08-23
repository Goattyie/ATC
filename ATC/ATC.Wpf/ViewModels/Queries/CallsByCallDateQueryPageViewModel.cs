using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CallsByCallDateQueryPageViewModel : AbstractQueryPageViewModel<DateInput, BaseCallResult>
    {
        public CallsByCallDateQueryPageViewModel(ICallsByCallDateQuery query) : base(query)
        {
        }
    }
}

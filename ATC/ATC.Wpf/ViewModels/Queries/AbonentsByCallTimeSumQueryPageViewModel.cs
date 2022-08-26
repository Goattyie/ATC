using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsByCallTimeSumQueryPageViewModel : AbstractQueryPageViewModel<TimeSpanInput, AbonentByCallTimeSumResult>
    {
        public AbonentsByCallTimeSumQueryPageViewModel(IAbonentsByCallTimeSumQuery query) : base(query)
        {
        }
    }
}

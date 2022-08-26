using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsByCallsCostSumQueryPageViewModel : AbstractQueryPageViewModel<DoubleInput, AbonentsByCallsCostSumResult>
    {
        public AbonentsByCallsCostSumQueryPageViewModel(IAbonentsByCallsCostSumQuery query) : base(query)
        {
        }
    }
}

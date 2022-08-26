using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsByCallsCostDateSumQueryPageViewModel : AbstractQueryPageViewModel<DoubleDateInput, AbonentsByCallsCostSumResult>
    {
        public AbonentsByCallsCostDateSumQueryPageViewModel(IAbonentsByCallsCostDateSumQuery query) : base(query)
        {
        }
    }
}

using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class DiffCallCostQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, DiffCallCostResult>
    {
        public DiffCallCostQueryPageViewModel(IDiffCallCostQuery query) : base(query)
        {
        }
    }
}

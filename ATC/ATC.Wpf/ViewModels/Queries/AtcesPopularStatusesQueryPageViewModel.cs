using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AtcesPopularStatusesQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, NamePopularStatusesResult>
    {
        public AtcesPopularStatusesQueryPageViewModel(IAtcesPopularStatusesQuery query) : base(query)
        {
        }
    }
}

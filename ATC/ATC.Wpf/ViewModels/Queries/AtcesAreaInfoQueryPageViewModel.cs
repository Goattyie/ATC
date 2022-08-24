using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AtcesAreaInfoQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AtcesAreaInfoResult>
    {
        public AtcesAreaInfoQueryPageViewModel(IAtcesAreaInfoQuery query) : base(query)
        {
        }
    }
}

using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class TimeCallsSumQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, TimeCallsSumResult>
    {
        public TimeCallsSumQueryPageViewModel(ITimeCallsSumQuery query) : base(query)
        {
        }
    }
}

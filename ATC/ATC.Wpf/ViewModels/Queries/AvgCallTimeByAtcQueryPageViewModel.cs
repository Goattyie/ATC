using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AvgCallTimeByAtcQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AvgCallTimeResult>
    {
        public AvgCallTimeByAtcQueryPageViewModel(IAvgCallTimeByAtcQuery query) : base(query)
        {
        }
    }
}

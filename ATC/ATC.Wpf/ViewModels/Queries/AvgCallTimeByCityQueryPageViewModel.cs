using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AvgCallTimeByCityQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AvgCallTimeResult>
    {
        public AvgCallTimeByCityQueryPageViewModel(IAvgCallTimeByCityQuery query) : base(query)
        {
        }
    }
}

using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class FirmsSumConnectionCostInflationQueryPageViewModel : AbstractQueryPageViewModel<TwoDatesInput, FirmsSumConnectionCostInflationResult>
    {
        public FirmsSumConnectionCostInflationQueryPageViewModel(IFirmsSumConnectionCostInflationQuery query) : base(query)
        {
        }
    }
}

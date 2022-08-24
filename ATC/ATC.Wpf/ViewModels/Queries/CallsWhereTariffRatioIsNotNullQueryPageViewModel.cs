using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CallsWhereTariffRatioIsNotNullQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, CallsWhereTariffRatioResult>
    {
        public CallsWhereTariffRatioIsNotNullQueryPageViewModel(ICallsWhereTariffRatioIsNotNullQuery query) : base(query)
        {
        }
    }
}

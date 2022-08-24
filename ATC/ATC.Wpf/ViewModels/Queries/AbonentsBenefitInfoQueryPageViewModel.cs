using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsBenefitInfoQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AbonentBenefitInfoResult>
    {
        public AbonentsBenefitInfoQueryPageViewModel(IAbonentsBenefitInfoQuery query) : base(query)
        {
        }
    }
}

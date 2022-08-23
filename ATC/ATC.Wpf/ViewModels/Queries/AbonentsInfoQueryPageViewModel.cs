using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsInfoQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AbonentInfoResult>
    {
        public AbonentsInfoQueryPageViewModel(IAbonentsInfoQuery query) : base(query)
        {
        }
    }
}

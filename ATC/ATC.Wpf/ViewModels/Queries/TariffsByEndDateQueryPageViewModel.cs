using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class TariffsByEndDateQueryPageViewModel : AbstractQueryPageViewModel<DateInput, BaseNameResult>
    {
        public TariffsByEndDateQueryPageViewModel(ITariffsByEndDateQuery query) : base(query)
        {
        }
    }
}

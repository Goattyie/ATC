using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsHaveCallsByPhoneQueryPageViewModel : AbstractQueryPageViewModel<StringInput, AbonentHaveCallsByPhoneResult>
    {
        public AbonentsHaveCallsByPhoneQueryPageViewModel(IAbonentsHaveCallsByPhoneQuery query) : base(query)
        {
        }
    }
}

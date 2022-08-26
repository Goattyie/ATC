using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CountAbonentsBySocialStatusQueryPageViewModel : AbstractQueryPageViewModel<StringInput, CountResult>
    {
        public CountAbonentsBySocialStatusQueryPageViewModel(ICountAbonentsBySocialStatusQuery query) : base(query)
        {
        }
    }
}

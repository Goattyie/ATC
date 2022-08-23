using ATC.Wpf.Models;
using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AbonentsBySocialStatusQueryPageViewModel : AbstractQueryPageViewModel<StringInput, AbonentBySocialResult>
    {
        public AbonentsBySocialStatusQueryPageViewModel(IAbonentsBySocialStatusQuery query) : base(query)
        {
        }
    }
}

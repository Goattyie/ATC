using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CaseQueryPageViewModel : AbstractQueryPageViewModel<StringInput, CaseResult>
    {
        public CaseQueryPageViewModel(ICaseQuery query) : base(query)
        {
        }
    }
}

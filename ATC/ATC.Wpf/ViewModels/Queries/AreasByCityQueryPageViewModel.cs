using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AreasByCityQueryPageViewModel : AbstractQueryPageViewModel<StringInput, BaseNameResult>
    {
        public AreasByCityQueryPageViewModel(IAreasByCityQuery query) : base(query)
        {
        }
    }
}

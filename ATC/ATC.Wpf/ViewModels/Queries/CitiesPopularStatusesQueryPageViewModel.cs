using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class CitiesPopularStatusesQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, NamePopularStatusesResult>
    {
        public CitiesPopularStatusesQueryPageViewModel(ICitiesPopularStatusesQuery query) : base(query)
        {
        }
    }
}

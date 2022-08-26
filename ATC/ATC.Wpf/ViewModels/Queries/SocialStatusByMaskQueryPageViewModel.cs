using ATC.Wpf.Queries.Interfaces;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class SocialStatusByMaskQueryPageViewModel : AbstractQueryPageViewModel<StringInput, SocialStatusByMaskResult>
    {
        public SocialStatusByMaskQueryPageViewModel(ISocialStatusByMaskQuery query) : base(query)
        {
        }
    }
}

using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class NamePopularStatusesResult
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("social_status")]
        public string SocialStatus { get; set; }
    }

    internal interface IAtcesPopularStatusesQuery : IQuery<BaseInput, NamePopularStatusesResult>
    {
    }
}

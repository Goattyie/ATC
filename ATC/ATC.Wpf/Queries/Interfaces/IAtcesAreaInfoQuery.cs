using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AtcesAreaInfoResult
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("address")]
        public string Address { get; set; }
        
        [DisplayName("area_name")]
        public string AreaName { get; set; }
    }

    internal interface IAtcesAreaInfoQuery : IQuery<BaseInput, AtcesAreaInfoResult>
    {
    }
}

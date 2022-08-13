using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class AtcModel : BaseModel
    {
        [DisplayName("code")]
        public string Code { get; set; }

        [DisplayName("name")]
        public string Name { get; set; }

        [DisplayName("area_id")]
        public int AreaId { get; set; }

        [DisplayName("area_name")]
        public string AreaName { get; set; }

        [DisplayName("address")]
        public string Address { get; set; }

        [DisplayName("open_year")]
        public int OpenYear { get; set; }
        
        [DisplayName("is_state")]
        public bool IsState { get; set; }

        [DisplayName("license")]
        public bool License { get; set; }
    }
}

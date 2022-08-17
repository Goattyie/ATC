using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class AreaModel : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }

        [DisplayName("city_id")]
        public int CityId { get; set; }

        [DisplayName("city_name")]
        public string CityName { get; set; }
    }
}

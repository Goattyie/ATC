using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class CityModel : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("country_id")]
        public int CountryId { get; set; }
        
        [DisplayName("country_name")]
        public string CountryName { get; set; }
    }
}

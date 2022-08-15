using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class CountryModel : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }
}

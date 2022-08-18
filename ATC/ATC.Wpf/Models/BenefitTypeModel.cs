using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class BenefitTypeModel : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }
}

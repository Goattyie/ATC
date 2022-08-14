using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class BenefitType : BaseModel
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }
}

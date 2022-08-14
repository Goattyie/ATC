using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class Benefit : BaseModel
    {
        [DisplayName("benefit_type_id")]
        public int BenefitTypeId { get; set; }
        
        [DisplayName("benefit_type_name")]
        public string BenetitTypeName { get; set; }
        
        [DisplayName("conditions")]
        public string Conditions { get; set; }
        
        [DisplayName("tariff")]
        public string Tariff { get; set; }
    }
}

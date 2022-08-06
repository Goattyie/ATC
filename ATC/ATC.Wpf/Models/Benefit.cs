namespace ATC.Wpf.Models
{
    internal class Benefit : BaseModel
    {
        public int BenefitTypeId { get; set; }
        public BenefitType BenetitType { get; set; }
        public string Conditions { get; set; }
        public string Tariff { get; set; }
        public string Photo { get; set; }
    }
}

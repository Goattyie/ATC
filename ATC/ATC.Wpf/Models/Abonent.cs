namespace ATC.Wpf.Models
{
    internal class Abonent : BaseModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int BenefitId { get; set; }
        public Benefit Benefit { get; set; }
        public int SocialStatusId { get; set; }
        public SocialStatus SocialStatus { get; set; }
    }
}

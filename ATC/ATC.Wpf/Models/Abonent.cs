using System.ComponentModel;

namespace ATC.Wpf.Models
{
    internal class Abonent : BaseModel
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }
        
        [DisplayName("second_name")]
        public string SecondName { get; set; }
        
        [DisplayName("last_name")]
        public string LastName { get; set; }
        
        [DisplayName("phone")]
        public string Phone { get; set; }
        
        [DisplayName("benefit_id")]
        public int BenefitId { get; set; }
        
        [DisplayName("benefit_conditions")]
        public string BenefitConditions { get; set; }
        
        [DisplayName("social_status_id")]
        public int SocialStatusId { get; set; }
        
        [DisplayName("social_status_name")]
        public string SocialStatusName { get; set; }
        
        [DisplayName("photo")]
        public string Photo { get; set; }
    }
}

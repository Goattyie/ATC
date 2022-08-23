using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AbonentInfoResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }
        
        [DisplayName("second_name")]
        public string SecondName { get; set; }
        
        [DisplayName("last_name")]
        public string LastName { get; set; }
        
        [DisplayName("phone")]
        public string Phone { get; set; }
        
        [DisplayName("social_status_name")]
        public string SocialStatus { get; set; }

    }

    internal interface IAbonentsInfoQuery : IQuery<BaseInput, AbonentInfoResult> 
    {
    }
}

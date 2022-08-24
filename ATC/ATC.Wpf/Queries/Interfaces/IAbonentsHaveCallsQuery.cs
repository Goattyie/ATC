using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AbonentHaveCallsResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }

        [DisplayName("second_name")]
        public string SecondName { get; set; }

        [DisplayName("last_name")]
        public string LastName { get; set; }

        [DisplayName("phone")]
        public string Phone { get; set; }
    }

    internal interface IAbonentsHaveCallsQuery : IQuery<BaseInput, AbonentHaveCallsResult>
    {
    }
}

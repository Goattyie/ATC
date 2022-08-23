using ATC.Wpf.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class StringInput : BaseInput
    {
        public string Value { get; set; }
    }

    internal class AbonentBySocialResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }

        [DisplayName("second_name")]
        public string SecondName { get; set; }

        [DisplayName("phone")]
        public string Phone { get; set; }
    }

    internal interface IAbonentsBySocialStatusQuery : IQuery<StringInput, AbonentBySocialResult>
    {
    }
}

using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class DoubleInput : BaseInput
    {
        public double Value { get; set; }
    }

    internal class AbonentsByCallsCostSumResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }

        [DisplayName("second_name")]
        public string SecondName { get; set; }

        [DisplayName("last_name")]
        public string LastName { get; set; }

        [DisplayName("phone")]
        public string Phone { get; set; }

        [DisplayName("sum")]
        public decimal Sum { get; set; }
    }

    internal interface IAbonentsByCallsCostSumQuery : IQuery<DoubleInput, AbonentsByCallsCostSumResult>
    {
    }
}

using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class TimeSpanInput : BaseInput
    {
        public TimeSpan Value { get; set; }
    }

    internal class AbonentByCallTimeSumResult
    {
        [DisplayName("first_name")]
        public string FirstName { get; set; }

        [DisplayName("second_name")]
        public string SecondName { get; set; }

        [DisplayName("last_name")]
        public string LastName { get; set; }

        [DisplayName("phone")]
        public string Phone { get; set; }

        [DisplayName("s_time")]
        public TimeSpan Sum { get; set; }
    }

    internal interface IAbonentsByCallTimeSumQuery : IQuery<TimeSpanInput, AbonentByCallTimeSumResult>
    {
    }
}

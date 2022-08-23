using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class BaseCallResult
    {
        [DisplayName("from")]
        public string From { get; set; }
        
        [DisplayName("to")]
        public string To { get; set; }
        
        [DisplayName("call_date")]
        public DateTime CallDate { get; set; }
    }

    internal class DateInput : BaseInput
    {
        public DateTime Value { get; set; } = DateTime.Now;
    }

    internal interface ICallsByCallDateQuery : IQuery<DateInput, BaseCallResult>
    {
    }
}

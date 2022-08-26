using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class CallsByAtcAreaNameResult
    {
        [DisplayName("from")]
        public string From { get; set; }
        
        [DisplayName("to")]
        public string To { get; set; }
        
        [DisplayName("call_date")]
        public DateTime CallDate { get; set; }
    }

    internal interface ICallsByAtcAreaNameQuery : IQuery<StringInput, CallsByAtcAreaNameResult>
    {
    }
}

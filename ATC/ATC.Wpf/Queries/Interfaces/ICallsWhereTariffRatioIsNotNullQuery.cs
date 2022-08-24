using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class CallsWhereTariffRatioResult
    {
        [DisplayName("from")]
        public string From { get; set; }
        
        [DisplayName("to")]
        public string To { get; set; }
        
        [DisplayName("call_date")]
        public DateTime CallDate { get; set; }
        
        [DisplayName("ratio")]
        public double Ratio { get; set; }
    }

    internal interface ICallsWhereTariffRatioIsNotNullQuery : IQuery<BaseInput, CallsWhereTariffRatioResult>
    {
    }
}

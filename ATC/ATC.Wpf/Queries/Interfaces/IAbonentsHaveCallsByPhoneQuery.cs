using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AbonentHaveCallsByPhoneResult : AbonentHaveCallsResult
    {
        [DisplayName("call_date")]
        public DateTime CallDate { get; set; }
    }

    internal interface IAbonentsHaveCallsByPhoneQuery : IQuery<StringInput, AbonentHaveCallsByPhoneResult>
    {
    }
}

using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class TimeCallsSumResult
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("address")]
        public string Address { get; set; }
        
        [DisplayName("Sum")]
        public TimeSpan Sum { get; set; }
    }

    internal interface ITimeCallsSumQuery : IQuery<BaseInput, TimeCallsSumResult>
    {
    }
}

using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class AvgCallTimeResult
    {
        [DisplayName("name")]
        public string Name { get; set; }
        
        [DisplayName("avg")]
        public TimeSpan Avg { get; set; }
    }

    internal interface IAvgCallTimeByAtcQuery : IQuery<BaseInput, AvgCallTimeResult>
    {
    }
}

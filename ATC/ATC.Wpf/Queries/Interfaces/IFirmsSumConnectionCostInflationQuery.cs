using System;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class TwoDatesInput : BaseInput
    {
        public DateTime FirstValue { get; set; } = DateTime.Now.AddDays(-1);
        public DateTime SecondValue { get; set; } = DateTime.Now;
    }

    internal class FirmsSumConnectionCostInflationResult
    {
        [DisplayName("name")]
        public string Name { get; set; }

        [DisplayName("before")]
        public decimal Before { get; set; }
        
        [DisplayName("after")]
        public decimal After { get; set; }
    }

    internal interface IFirmsSumConnectionCostInflationQuery : IQuery<TwoDatesInput, FirmsSumConnectionCostInflationResult>
    {
    }
}

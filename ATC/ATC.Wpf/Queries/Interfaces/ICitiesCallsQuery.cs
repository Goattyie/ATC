using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class CallsResult
    {
        [DisplayName("count")]
        public long Count { get; set; }
        
        [DisplayName("sum")]
        public decimal Sum { get; set; }
    }
    internal interface ICitiesCallsQuery : IQuery<BaseInput, CallsResult>
    {
    }
}

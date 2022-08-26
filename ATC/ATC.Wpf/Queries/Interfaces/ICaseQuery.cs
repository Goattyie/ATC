using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class CaseResult
    {
        [DisplayName("cost")]
        public double Cost { get; set; }
    }

    internal interface ICaseQuery : IQuery<StringInput, CaseResult>
    {
    }
}

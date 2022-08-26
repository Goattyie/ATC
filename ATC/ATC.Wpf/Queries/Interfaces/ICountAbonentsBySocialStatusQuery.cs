using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class CountResult
    {
        [DisplayName("count")]
        public long Count { get; set; }
    }

    internal interface ICountAbonentsBySocialStatusQuery : IQuery<StringInput, CountResult>
    {
    }
}

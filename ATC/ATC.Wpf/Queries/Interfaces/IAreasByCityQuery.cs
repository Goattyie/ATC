using ATC.Wpf.Models;
using System.ComponentModel;

namespace ATC.Wpf.Queries.Interfaces
{
    public class BaseNameResult
    {
        [DisplayName("name")]
        public string Name { get; set; }
    }

    internal interface IAreasByCityQuery : IQuery<StringInput, BaseNameResult>
    {
    }
}

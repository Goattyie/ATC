using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class BaseInput { }

    internal interface IQuery<TInput, TOutput>
        where TInput : BaseInput
    {
        Task<IEnumerable<TOutput>> ExecuteQuery(TInput input);
    }
}

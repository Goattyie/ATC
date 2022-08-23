using ATC.Wpf.Extensions;
using ATC.Wpf.Queries.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Queries
{
    internal abstract class AbstractQuery<TInput, TOutput> : IQuery<TInput, TOutput>
        where TInput : BaseInput
    {
        protected NpgsqlConnection Connection { get; private set; }

        public AbstractQuery(NpgsqlConnection connection)
        {
            Connection = connection;
        }

        protected abstract string GenerateQuery(TInput data);

        public async Task<IEnumerable<TOutput>> ExecuteQuery(TInput input)
        {
            await Connection.OpenAsync();

            await using var cmd = new NpgsqlCommand(GenerateQuery(input), Connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            var list = await reader.ReadToList<TOutput>();

            await Connection.CloseAsync();

            return list;
        }
    }
}

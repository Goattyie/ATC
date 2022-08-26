using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CallsByAtcAreaNameQuery : AbstractQuery<StringInput, CallsByAtcAreaNameResult>, ICallsByAtcAreaNameQuery
    {
        public CallsByAtcAreaNameQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_calls_by_atc_area_name('{data.Value}');";
    }
}

using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CallsNoByAtcAreaNameQuery : AbstractQuery<StringInput, CallsByAtcAreaNameResult>, ICallsNoByAtcAreaNameQuery
    {
        public CallsNoByAtcAreaNameQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_calls_no_by_atc_area_name('{data.Value}');";
    }
}

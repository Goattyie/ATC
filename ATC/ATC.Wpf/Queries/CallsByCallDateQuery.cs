using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CallsByCallDateQuery : AbstractQuery<DateInput, BaseCallResult>, ICallsByCallDateQuery
    {
        public CallsByCallDateQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(DateInput data) => $"SELECT * FROM get_calls_by_call_date('{data.Value.ToString("yyyy-MM-dd")}')";
    }
}

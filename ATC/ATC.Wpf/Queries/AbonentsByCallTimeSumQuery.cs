using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsByCallTimeSumQuery : AbstractQuery<TimeSpanInput, AbonentByCallTimeSumResult>, IAbonentsByCallTimeSumQuery
    {
        public AbonentsByCallTimeSumQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(TimeSpanInput data) => $"SELECT * FROM get_abonents_by_call_time_sum('{data.Value}')";
    }
}

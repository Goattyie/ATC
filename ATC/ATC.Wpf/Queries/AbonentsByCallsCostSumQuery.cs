using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsByCallsCostSumQuery : AbstractQuery<DoubleInput, AbonentsByCallsCostSumResult>, IAbonentsByCallsCostSumQuery
    {
        public AbonentsByCallsCostSumQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(DoubleInput data) => $"SELECT * FROM get_abonents_by_calls_cost_sum({data.Value});";
    }
}

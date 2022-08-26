using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsByCallsCostDateSumQuery : AbstractQuery<DoubleDateInput, AbonentsByCallsCostSumResult>, IAbonentsByCallsCostDateSumQuery
    {
        public AbonentsByCallsCostDateSumQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(DoubleDateInput data) => $"SELECT * FROM get_abonents_by_calls_cost_date_sum({data.DoubleValue}, '{data.DateValue.ToString("yyyy-MM-dd")}');";
    }
}
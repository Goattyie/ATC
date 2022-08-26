using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class FirmsSumConnectionCostInflationQuery : AbstractQuery<TwoDatesInput, FirmsSumConnectionCostInflationResult>, IFirmsSumConnectionCostInflationQuery
    {
        public FirmsSumConnectionCostInflationQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(TwoDatesInput data) => $"SELECT * FROM get_firms_sum_connection_cost_inflation('{data.FirstValue.ToString("yyyy-MM-dd")}', '{data.SecondValue.ToString("yyyy-MM-dd")}');";
    }
}

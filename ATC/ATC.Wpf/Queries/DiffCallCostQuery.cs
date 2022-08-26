using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class DiffCallCostQuery : AbstractQuery<BaseInput, DiffCallCostResult>, IDiffCallCostQuery
    {
        public DiffCallCostQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_diff_call_cost;";
    }
}

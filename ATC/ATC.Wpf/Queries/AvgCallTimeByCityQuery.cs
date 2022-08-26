using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AvgCallTimeByCityQuery : AbstractQuery<BaseInput, AvgCallTimeResult>, IAvgCallTimeByCityQuery
    {
        public AvgCallTimeByCityQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_avg_call_time_by_city;";
    }
}

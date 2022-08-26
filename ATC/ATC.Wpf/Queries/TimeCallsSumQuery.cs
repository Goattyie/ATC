using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class TimeCallsSumQuery : AbstractQuery<BaseInput, TimeCallsSumResult>, ITimeCallsSumQuery
    {
        public TimeCallsSumQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_time_calls_sum";
    }
}

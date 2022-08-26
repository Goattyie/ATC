using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AtcesPopularStatusesQuery : AbstractQuery<BaseInput, NamePopularStatusesResult>, IAtcesPopularStatusesQuery
    {
        public AtcesPopularStatusesQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_atces_popular_statuses;";
    }
}

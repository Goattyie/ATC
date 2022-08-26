using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CitiesPopularStatusesQuery : AbstractQuery<BaseInput, NamePopularStatusesResult>, ICitiesPopularStatusesQuery
    {
        public CitiesPopularStatusesQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_cities_popular_statuses;";
    }
}
using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CitiesCallsQuery : AbstractQuery<BaseInput, CallsResult>, ICitiesCallsQuery
    {
        public CitiesCallsQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_cities_calls;";
    }
}

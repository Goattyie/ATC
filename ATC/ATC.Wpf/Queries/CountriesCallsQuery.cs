using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CountriesCallsQuery : AbstractQuery<BaseInput, CallsResult>, ICountriesCallsQuery
    {
        public CountriesCallsQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_countries_calls;";
    }
}

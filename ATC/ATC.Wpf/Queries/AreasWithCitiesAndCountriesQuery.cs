using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AreasWithCitiesAndCountriesQuery : AbstractQuery<BaseInput, AreasWithCitiesAndCountriesResult>, IAreasWithCitiesAndCountriesQuery
    {
        public AreasWithCitiesAndCountriesQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => $"SELECT * FROM get_areas_with_cities_and_counties;";
    }
}

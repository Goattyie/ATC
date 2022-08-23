using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AreasByCityQuery : AbstractQuery<StringInput, BaseNameResult>, IAreasByCityQuery
    {
        public AreasByCityQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_areas_by_city('{data.Value}');";
    }
}

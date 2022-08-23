using ATC.Wpf.Models;
using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsBySocialStatusQuery : AbstractQuery<StringInput, AbonentBySocialResult>, IAbonentsBySocialStatusQuery
    {
        public AbonentsBySocialStatusQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput input) => $"SELECT * FROM get_abonents_by_social_status('{input.Value}')";
    }
}

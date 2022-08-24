using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsHaveCallsQuery : AbstractQuery<BaseInput, AbonentHaveCallsResult>, IAbonentsHaveCallsQuery
    {
        public AbonentsHaveCallsQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_abonents_have_calls";
    }
}

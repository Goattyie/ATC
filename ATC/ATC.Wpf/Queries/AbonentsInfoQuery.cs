using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsInfoQuery : AbstractQuery<BaseInput, AbonentInfoResult>, IAbonentsInfoQuery
    {
        public AbonentsInfoQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_abonents_info";
    }
}

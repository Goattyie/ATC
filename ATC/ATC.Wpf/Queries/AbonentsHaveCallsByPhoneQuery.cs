using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsHaveCallsByPhoneQuery : AbstractQuery<StringInput, AbonentHaveCallsByPhoneResult>, IAbonentsHaveCallsByPhoneQuery
    {
        public AbonentsHaveCallsByPhoneQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_abonents_have_calls_by_phone('{data.Value}')";
    }
}

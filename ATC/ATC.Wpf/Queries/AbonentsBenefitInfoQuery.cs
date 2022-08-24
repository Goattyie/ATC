using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AbonentsBenefitInfoQuery : AbstractQuery<BaseInput, AbonentBenefitInfoResult>, IAbonentsBenefitInfoQuery
    {
        public AbonentsBenefitInfoQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_abonents_benefit_info";
    }
}

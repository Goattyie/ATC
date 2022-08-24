using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CallsWhereTariffRatioIsNotNullQuery : AbstractQuery<BaseInput, CallsWhereTariffRatioResult>, ICallsWhereTariffRatioIsNotNullQuery
    {
        public CallsWhereTariffRatioIsNotNullQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_calls_where_tariff_ratio_is_not_null";
    }
}

using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class TariffsByEndDateQuery : AbstractQuery<DateInput, BaseNameResult>, ITariffsByEndDateQuery
    {
        public TariffsByEndDateQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(DateInput data) => $"SELECT * FROM get_tariffs_by_end_date('{data.Value.ToString("yyyy-MM-dd")}')";
    }
}

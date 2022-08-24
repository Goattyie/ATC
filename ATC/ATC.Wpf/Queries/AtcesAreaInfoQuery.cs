using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class AtcesAreaInfoQuery : AbstractQuery<BaseInput, AtcesAreaInfoResult>, IAtcesAreaInfoQuery
    {
        public AtcesAreaInfoQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(BaseInput data) => "SELECT * FROM get_atces_area_info";
    }
}

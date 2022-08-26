using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class SocialStatusByMaskQuery : AbstractQuery<StringInput, SocialStatusByMaskResult>, ISocialStatusByMaskQuery
    {
        public SocialStatusByMaskQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_social_status_by_mask('{data.Value}');";
    }
}

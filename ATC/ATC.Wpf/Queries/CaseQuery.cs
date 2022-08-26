using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CaseQuery : AbstractQuery<StringInput, CaseResult>, ICaseQuery
    {
        public CaseQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM case_query('{data.Value}');";
    }
}

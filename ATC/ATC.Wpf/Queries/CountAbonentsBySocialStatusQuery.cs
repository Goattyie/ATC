﻿using ATC.Wpf.Queries.Interfaces;
using Npgsql;

namespace ATC.Wpf.Queries
{
    internal class CountAbonentsBySocialStatusQuery : AbstractQuery<StringInput, CountResult>, ICountAbonentsBySocialStatusQuery
    {
        public CountAbonentsBySocialStatusQuery(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string GenerateQuery(StringInput data) => $"SELECT * FROM get_count_abonents_by_social_status('{data.Value}')";
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class TariffRepository : AbstractRepository<Tariff>, ITariffRepository
    {
        public TariffRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => throw new System.NotImplementedException();

        protected override async Task OnCreate(Tariff model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO tariffs (name, start_date, end_date, ratio) VALUES (@name, @start_date, @end_date, @ratio)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("start_date", model.StartDate);
            cmd.Parameters.AddWithValue("end_date", model.EndDate);
            cmd.Parameters.AddWithValue("ratio", model.Ratio);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('tariffs_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override Task OnDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnUpdate(Tariff model)
        {
            throw new System.NotImplementedException();
        }
    }
}

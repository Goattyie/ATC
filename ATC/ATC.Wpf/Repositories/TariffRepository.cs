using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class TariffRepository : AbstractRepository<TariffModel>, ITariffRepository
    {
        public TariffRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT * FROM tariffs";

        protected override async Task OnCreate(TariffModel model)
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

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM tariffs WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(TariffModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE tariffs SET name = @name, ratio = @ratio, start_date = @start_date, end_date = @end_date " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("ratio", model.Ratio);
            cmd.Parameters.AddWithValue("start_date", model.StartDate);
            cmd.Parameters.AddWithValue("end_date", model.EndDate);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

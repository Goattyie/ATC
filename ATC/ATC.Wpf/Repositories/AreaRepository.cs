using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AreaRepository : AbstractRepository<Area>, IAreaRepository
    {
        public AreaRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Area model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO areas (name, city_id) VALUES (@name, @c_id)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("c_id", model.CityId);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT id FROM areas WHERE name = @name AND city_id = @c_id", Connection);

            cmd2.Parameters.AddWithValue("name", model.Name);
            cmd2.Parameters.AddWithValue("c_id", model.CityId);

            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

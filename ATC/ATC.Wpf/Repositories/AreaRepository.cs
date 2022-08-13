using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AreaRepository : AbstractRepository<Area>, IAreaRepository
    {
        public AreaRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT areas.id as id, areas.name as name, city.id as city_id, city.name as city_name " +
            "FROM areas " +
            "JOIN cities city ON city.id = areas.city_id";

        protected override async Task OnCreate(Area model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO areas (name, city_id) VALUES (@name, @c_id)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("c_id", model.CityId);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT currval('areas_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override Task OnDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnUpdate(Area model)
        {
            throw new System.NotImplementedException();
        }
    }
}

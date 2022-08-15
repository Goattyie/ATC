using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class CityRepository : AbstractRepository<CityModel>, ICityRepository
    {

        public CityRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection) { }

        protected override string SelectQuery => "SELECT city.id AS id, city.name AS name, country_id, country.name AS country_name " +
            "FROM cities city " +
            "JOIN countries country ON country.id = city.country_id " +
            "ORDER BY city.name";

        protected override async Task OnCreate(CityModel model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO cities (name, country_id) VALUES (@name, @c_id)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("c_id", model.CountryId);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('cities_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM cities WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(CityModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE cities SET name = @name, country_id = @country_id " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("country_id", model.CountryId);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

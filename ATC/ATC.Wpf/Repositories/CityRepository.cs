using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class CityRepository : AbstractRepository<City>, ICityRepository
    {

        public CityRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection) { }

        protected override string SelectQuery => throw new System.NotImplementedException();

        protected override async Task OnCreate(City model)
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

        protected override Task OnDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnUpdate(City model)
        {
            throw new System.NotImplementedException();
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class CountryRepository : AbstractRepository<Country>, ICountryRepository
    {
        public CountryRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Country model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO countries (name) VALUES (@name)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT currval('countries_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

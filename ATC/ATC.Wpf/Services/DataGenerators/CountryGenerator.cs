using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class CountryGenerator
    {
        private readonly ICountryRepository _repository;
        private readonly NpgsqlConnection _connection;

        public CountryGenerator(ICountryRepository repository, NpgsqlConnection connection)
        {
            _repository = repository;
            _connection = connection;
        }

        public async Task Generate()
        {
            await Clear(_connection);
            await GenerateData();
        }

        private async Task Clear(NpgsqlConnection connection)
        {
            await _connection.OpenAsync();

            var cmd = connection.CreateCommand();

            cmd.CommandText = "TRUNCATE countries CASCADE; ALTER SEQUENCE countries_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new Country { Name = "Россия" });
            await _repository.Create(new Country { Name = "Украина" });
            await _repository.Create(new Country { Name = "Беларусь" });
        }
    }
}

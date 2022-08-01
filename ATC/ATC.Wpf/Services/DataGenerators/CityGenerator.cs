using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class CityGenerator
    {
        private readonly ICityRepository _repository;
        private readonly NpgsqlConnection _connection;

        public CityGenerator(ICityRepository repository, NpgsqlConnection connection)
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

            cmd.CommandText = "TRUNCATE cities CASCADE; ALTER SEQUENCE cities_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new City { Name = "Таганрог", CountryId = 1 });
            await _repository.Create(new City { Name = "Ростов-На-Дону", CountryId = 1 });
            await _repository.Create(new City { Name = "Краснодар", CountryId = 1 });
            await _repository.Create(new City { Name = "Москва", CountryId = 1 });
            await _repository.Create(new City { Name = "Киев", CountryId = 2 });
            await _repository.Create(new City { Name = "Харьков", CountryId = 2 });
            await _repository.Create(new City { Name = "Херсон", CountryId = 2 });
            await _repository.Create(new City { Name = "Минск", CountryId = 3 });
        }
    }
}

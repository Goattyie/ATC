using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class AreaGenerator
    {
        private readonly IAreaRepository _repository;
        private readonly NpgsqlConnection _connection;

        public AreaGenerator(IAreaRepository repository, NpgsqlConnection connection)
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

            cmd.CommandText = "DELETE FROM areas; ALTER SEQUENCE areas_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new Area { Name = "Ленинский", CityId = 1 });
            await _repository.Create(new Area { Name = "Кировский", CityId = 2 });
            await _repository.Create(new Area { Name = "Калининский", CityId = 3 });
            await _repository.Create(new Area { Name = "Ворошиловский", CityId = 4 });
            await _repository.Create(new Area { Name = "Петровский", CityId = 5 });
        }
    }
}

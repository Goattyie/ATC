using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class TariffGenerator
    {
        private readonly ITariffRepository _repository;
        private readonly NpgsqlConnection _connection;

        public TariffGenerator(ITariffRepository repository, NpgsqlConnection connection)
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

            cmd.CommandText = "TRUNCATE tariffs CASCADE; ALTER SEQUENCE tariffs_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new Tariff { Name = "Безлимит" });
            await _repository.Create(new Tariff { Name = "Быстрый" });
            await _repository.Create(new Tariff { Name = "Комфортный" });
            await _repository.Create(new Tariff { Name = "Эконом" });
        }
    }
}

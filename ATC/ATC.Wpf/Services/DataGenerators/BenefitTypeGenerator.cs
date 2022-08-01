using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class BenefitTypeGenerator
    {
        private readonly IBenefitTypeRepository _repository;
        private readonly NpgsqlConnection _connection;

        public BenefitTypeGenerator(IBenefitTypeRepository repository, NpgsqlConnection connection)
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

            cmd.CommandText = "TRUNCATE benefit_types CASCADE; ALTER SEQUENCE benefit_types_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new BenefitType { Name = "Обычный" });
            await _repository.Create(new BenefitType { Name = "Экстра" });
            await _repository.Create(new BenefitType { Name = "Высочайший" });
        }
    }
}

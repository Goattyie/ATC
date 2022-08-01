using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class SocialStatusGenerator
    {
        private readonly ISocialStatusRepository _repository;
        private readonly NpgsqlConnection _connection;

        public SocialStatusGenerator(ISocialStatusRepository repository, NpgsqlConnection connection)
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

            cmd.CommandText = "TRUNCATE social_statuses CASCADE; ALTER SEQUENCE social_statuses_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

        }

        private async Task GenerateData()
        {
            await _repository.Create(new SocialStatus { Name = "Попрошайка" });
            await _repository.Create(new SocialStatus { Name = "Таксист" });
            await _repository.Create(new SocialStatus { Name = "Безработный" });
            await _repository.Create(new SocialStatus { Name = "Врач" });
            await _repository.Create(new SocialStatus { Name = "Охранник" });
            await _repository.Create(new SocialStatus { Name = "Бизнесмен" });
            await _repository.Create(new SocialStatus { Name = "Военный" });
            await _repository.Create(new SocialStatus { Name = "Полицейский" });
            await _repository.Create(new SocialStatus { Name = "Слесарь" });
            await _repository.Create(new SocialStatus { Name = "Сотрудник ГАИ" });
            await _repository.Create(new SocialStatus { Name = "Уборщик" });
            await _repository.Create(new SocialStatus { Name = "Учитель" });
            await _repository.Create(new SocialStatus { Name = "Секретарь" });
            await _repository.Create(new SocialStatus { Name = "Директор фирмы" });
            await _repository.Create(new SocialStatus { Name = "Мастер маникюра" });
            await _repository.Create(new SocialStatus { Name = "Программист" });
        }
    }
}

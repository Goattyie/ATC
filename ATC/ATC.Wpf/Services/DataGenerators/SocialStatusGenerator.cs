using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class SocialStatusGenerator : AbstractGenerator<SocialStatusModel>
    {
        public static int Count = 16;

        public SocialStatusGenerator(ISocialStatusRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE social_statuses CASCADE; ALTER SEQUENCE social_statuses_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new SocialStatusModel { Name = "Попрошайка" });
            await Repository.Create(new SocialStatusModel { Name = "Таксист" });
            await Repository.Create(new SocialStatusModel { Name = "Безработный" });
            await Repository.Create(new SocialStatusModel { Name = "Врач" });
            await Repository.Create(new SocialStatusModel { Name = "Охранник" });
            await Repository.Create(new SocialStatusModel { Name = "Бизнесмен" });
            await Repository.Create(new SocialStatusModel { Name = "Военный" });
            await Repository.Create(new SocialStatusModel { Name = "Полицейский" });
            await Repository.Create(new SocialStatusModel { Name = "Слесарь" });
            await Repository.Create(new SocialStatusModel { Name = "Сотрудник ГАИ" });
            await Repository.Create(new SocialStatusModel { Name = "Уборщик" });
            await Repository.Create(new SocialStatusModel { Name = "Учитель" });
            await Repository.Create(new SocialStatusModel { Name = "Секретарь" });
            await Repository.Create(new SocialStatusModel { Name = "Директор фирмы" });
            await Repository.Create(new SocialStatusModel { Name = "Мастер маникюра" });
            await Repository.Create(new SocialStatusModel { Name = "Программист" });
        }
    }
}

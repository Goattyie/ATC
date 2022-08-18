using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class BenefitTypeGenerator : AbstractGenerator<BenefitTypeModel>
    {
        public static int Count = 7;

        public BenefitTypeGenerator(IBenefitTypeRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE benefit_types CASCADE; ALTER SEQUENCE benefit_types_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new BenefitTypeModel { Name = "Обычный" });
            await Repository.Create(new BenefitTypeModel { Name = "Экстра" });
            await Repository.Create(new BenefitTypeModel { Name = "Высочайший" });
            await Repository.Create(new BenefitTypeModel { Name = "Удобный" });
            await Repository.Create(new BenefitTypeModel { Name = "Скромный" });
            await Repository.Create(new BenefitTypeModel { Name = "VIP" });
            await Repository.Create(new BenefitTypeModel { Name = "Лучший" });
        }
    }
}

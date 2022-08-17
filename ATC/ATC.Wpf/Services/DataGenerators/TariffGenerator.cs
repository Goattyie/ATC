using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class TariffGenerator : AbstractGenerator<TariffModel>
    {
        public static int Count = 4;

        public TariffGenerator(ITariffRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE tariffs CASCADE; ALTER SEQUENCE tariffs_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new TariffModel { Name = "Безлимит", StartDate = DateTime.UtcNow.AddDays(-100), EndDate = DateTime.UtcNow.AddDays(100), Ratio = 1 });
            await Repository.Create(new TariffModel { Name = "Быстрый", StartDate = DateTime.UtcNow.AddDays(-100), EndDate = DateTime.UtcNow.AddDays(100), Ratio = 1.15 });
            await Repository.Create(new TariffModel { Name = "Комфортный", StartDate = DateTime.UtcNow.AddDays(-100), EndDate = DateTime.UtcNow.AddDays(100), Ratio = 1.2 });
            await Repository.Create(new TariffModel { Name = "Эконом", StartDate = DateTime.UtcNow.AddDays(-100), EndDate = DateTime.UtcNow.AddDays(100), Ratio = 1.3 });
        }
    }
}

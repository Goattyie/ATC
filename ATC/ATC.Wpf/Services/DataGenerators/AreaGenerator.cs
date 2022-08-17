using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class AreaGenerator : AbstractGenerator<AreaModel>
    {
        public static int Count = 5;


        public AreaGenerator(IAreaRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DELETE FROM areas; ALTER SEQUENCE areas_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new AreaModel { Name = "Ленинский", CityId = 1 });
            await Repository.Create(new AreaModel { Name = "Кировский", CityId = 2 });
            await Repository.Create(new AreaModel { Name = "Калининский", CityId = 3 });
            await Repository.Create(new AreaModel { Name = "Ворошиловский", CityId = 4 });
            await Repository.Create(new AreaModel { Name = "Петровский", CityId = 5 });
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class CountryGenerator : AbstractGenerator<Country>
    {
        public CountryGenerator(ICountryRepository repository, NpgsqlConnection connection)
            : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE countries CASCADE; ALTER SEQUENCE countries_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new Country { Name = "Россия" });
            await Repository.Create(new Country { Name = "Украина" });
            await Repository.Create(new Country { Name = "Беларусь" });
        }
    }
}

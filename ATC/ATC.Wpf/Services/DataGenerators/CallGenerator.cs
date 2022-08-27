using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class CallGenerator : AbstractGenerator<CallModel>
    {
        public static int Count = 10000;

        public CallGenerator(ICallRepository repository, NpgsqlConnection connection) : base(repository, connection)
        {
        }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DELETE FROM calls; ALTER SEQUENCE calls_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        protected override async Task GenerateData()
        {
            var random = new Random();

            for(int i = 0; i < Count; i++)
            {
                await Repository.Create(new CallModel
                {
                    AtcId = random.Next(1, AtcGenerator.Count),
                    CallDate = DateTime.UtcNow.AddDays(-random.Next(1, 10000)),
                    Time = TimeSpan.FromMinutes(random.Next(1, 59)) + TimeSpan.FromSeconds(random.Next(1, 59)),
                    AbonentId = random.Next(1, AbonentGenerator.Count + 1),
                    CityId = random.Next(1, CityGenerator.Count + 1),
                    TariffId = random.Next(1, TariffGenerator.Count + 1),
                    Cost = random.Next(1, 300),
                    Phone = $"+7{random.NextInt64(1000000000, 9999999999)}",
                });
            }
        }
    }
}

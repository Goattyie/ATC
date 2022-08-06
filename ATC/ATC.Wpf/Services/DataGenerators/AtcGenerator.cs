using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class AtcGenerator : AbstractGenerator<Atc>
    {
        public static int Count = 10000;

        private readonly string[] _streets = new string[]
        {
            "Куйбышева",
            "Кирова",
            "Сергея Шило",
            "Чехова",
            "Петровского",
            "Карла Либкнехта"
        };

        public AtcGenerator(IAtcRepository repository, NpgsqlConnection connection) : base(repository, connection)
        {
        }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DELETE FROM atces; ALTER SEQUENCE atces_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        protected override async Task GenerateData()
        {
            var random = new Random();

            for(int i = 0; i < Count; i++)
            {
                await Repository.Create(new Atc
                {
                    Code = $"#{random.Next(1, 10000000)}",
                    Name = $"ATC {random.Next()}",
                    Address = $"ул. {_streets[random.Next(_streets.Length - 1)]} д. {random.Next(1, 1000)}",
                    AreaId = random.Next(1, AreaGenerator.Count),
                    IsState = random.Next() % 2 == 0,
                    License = random.Next() % 2 == 0,
                    OpenYear = random.Next(1500, DateTime.Now.Year)
                });
            }
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class BenefitGenerator : AbstractGenerator<Benefit>
    {
        public static int Count = 10000;

        public BenefitGenerator(IBenefitRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE benefits CASCADE; ALTER SEQUENCE benefits_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            var random = new Random();

            for (int i = 0; i < Count; i++)
                await Repository.Create(new Benefit 
                { 
                    BenefitTypeId = random.Next(1, BenefitTypeGenerator.Count), 
                    Conditions = $"Условие #{random.Next(1000)}", 
                    Tariff = $"Тариф #{random.Next(100)}", 
                });
        }
    }
}

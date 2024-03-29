﻿using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class CityGenerator : AbstractGenerator<CityModel>
    {
        public static int Count = 8;

        public CityGenerator(ICityRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "TRUNCATE cities CASCADE; ALTER SEQUENCE cities_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

        }

        protected override async Task GenerateData()
        {
            await Repository.Create(new CityModel { Name = "Таганрог", CountryId = 1 });
            await Repository.Create(new CityModel { Name = "Ростов-На-Дону", CountryId = 1 });
            await Repository.Create(new CityModel { Name = "Краснодар", CountryId = 1 });
            await Repository.Create(new CityModel { Name = "Москва", CountryId = 1 });
            await Repository.Create(new CityModel { Name = "Киев", CountryId = 2 });
            await Repository.Create(new CityModel { Name = "Харьков", CountryId = 2 });
            await Repository.Create(new CityModel { Name = "Херсон", CountryId = 2 });
            await Repository.Create(new CityModel { Name = "Минск", CountryId = 3 });
        }
    }
}

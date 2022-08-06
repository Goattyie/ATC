using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal class AbonentGenerator : AbstractGenerator<Abonent>
    {
        public static int Count = 10000;

        private string[] _maleNames = new string[]
        {
            "Олег",
            "Владислав",
            "Михаил",
            "Сергей",
            "Иван",
            "Петр",
            "Валерий"
        };
        private string[] _femaleNames = new string[]
        {
            "Екатерина",
            "Вероника",
            "Елизавета",
            "Анна"
        };
        private string[] _maleSecondNames = new string[]
        {
            "Петров",
            "Иванов",
            "Сидоров",
            "Сикорский",
            "Глебов",
            "Витов",
            "Сергеев",
            "Хоров",
            "Артемик",
            "Вариев"
        };
        private string[] _femaleSecondNames = new string[]
        {
            "Петрова",
            "Иванова",
            "Сидорова",
            "Сикорская",
            "Глебова",
            "Витова",
            "Сергеева",
            "Хорова",
            "Артемьева",
            "Вариева"
        };
        private string[] _maleLastNames = new string[]
        {
            "Олегович",
            "Владиславович",
            "Михаилович",
            "Сергеевич",
            "Иванович",
            "Петрович",
            "Валерьевич"
        };
        private string[] _femaleLastNames = new string[]
        {
            "Олеговна",
            "Владиславовна",
            "Михаиловна",
            "Сергеевна",
            "Иванона",
            "Петровна",
            "Валерьевна"
        };

        public AbonentGenerator(IAbonentRepository repository, NpgsqlConnection connection) : base(repository, connection) { }

        protected override async Task Clear()
        {
            await Connection.OpenAsync();

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DELETE FROM abonents; ALTER SEQUENCE abonents_id_seq RESTART WITH 1;";

            await cmd.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        protected override async Task GenerateData()
        {
            var random = new Random();

            for(int i = 0; i < Count; i++)
            {
                await Repository.Create(new Abonent 
                { 
                    FirstName = _maleNames[random.Next(_maleNames.Length)], 
                    SecondName = _maleSecondNames[random.Next(_maleSecondNames.Length)],
                    LastName = _maleLastNames[random.Next(_maleLastNames.Length)],
                    Phone = $"+7{random.NextInt64(1000000000, 9999999999)}",
                    BenefitId = random.Next(1, BenefitGenerator.Count),
                    SocialStatusId = random.Next(1, SocialStatusGenerator.Count)
                });
            }
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AbonentRepository : AbstractRepository<Abonent>, IAbonentRepository
    {
        public AbonentRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT abonent.id AS id, first_name, second_name, last_name, phone, benefit_id, benefit.conditions AS benefit_conditions, social_status_id, social_status.name AS social_status_name, photo " +
            "FROM abonents abonent " +
            "JOIN benefits benefit ON benefit.id = abonent.benefit_id " +
            "JOIN social_statuses social_status ON social_status.id = abonent.social_status_id";

        protected override async Task OnCreate(Abonent model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO abonents (first_name, second_name, last_name, phone, benefit_id, social_status_id, photo) " +
                "VALUES (@f_n, @s_n, @l_n, @phone, @b_id, @s_s_id, @photo)", Connection);

            cmd.Parameters.AddWithValue("f_n", model.FirstName);
            cmd.Parameters.AddWithValue("s_n", model.SecondName);
            cmd.Parameters.AddWithValue("l_n", model.LastName);
            cmd.Parameters.AddWithValue("phone", model.Phone);
            cmd.Parameters.AddWithValue("b_id", model.BenefitId);
            cmd.Parameters.AddWithValue("s_s_id", model.SocialStatusId);
            cmd.Parameters.AddWithValue("photo", model.Photo);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT currval('abonents_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override Task OnDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnUpdate(Abonent model)
        {
            throw new System.NotImplementedException();
        }
    }
}

﻿using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AbonentRepository : AbstractRepository<AbonentModel>, IAbonentRepository
    {
        public AbonentRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT abonent.id AS id, first_name, second_name, last_name, phone, benefit_id, benefit.conditions AS benefit_conditions, social_status_id, social_status.name AS social_status_name, photo " +
            "FROM abonents abonent " +
            "JOIN benefits benefit ON benefit.id = abonent.benefit_id " +
            "JOIN social_statuses social_status ON social_status.id = abonent.social_status_id " +
            "ORDER BY abonent.id";

        protected override async Task OnCreate(AbonentModel model)
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

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM abonents WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(AbonentModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE abonents SET first_name = @first_name, second_name = @second_name, last_name = @last_name, phone = @phone, photo = @photo, benefit_id = @benefit_id, social_status_id = @social_status_id " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("first_name", model.FirstName);
            cmd.Parameters.AddWithValue("second_name", model.SecondName);
            cmd.Parameters.AddWithValue("last_name", model.LastName);
            cmd.Parameters.AddWithValue("phone", model.Phone);
            cmd.Parameters.AddWithValue("photo", model.Photo);
            cmd.Parameters.AddWithValue("benefit_id", model.BenefitId);
            cmd.Parameters.AddWithValue("social_status_id", model.SocialStatusId);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

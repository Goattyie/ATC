using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AbonentRepository : AbstractRepository<Abonent>, IAbonentRepository
    {
        public AbonentRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Abonent model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO abonents (first_name, second_name, last_name, phone, benefit_id, social_status_id) " +
                "VALUES (@f_n, @s_n, @l_n, @phone, @b_id, @s_s_id)", Connection);

            cmd.Parameters.AddWithValue("f_n", model.FirstName);
            cmd.Parameters.AddWithValue("s_n", model.SecondName);
            cmd.Parameters.AddWithValue("l_n", model.LastName);
            cmd.Parameters.AddWithValue("phone", model.Phone);
            cmd.Parameters.AddWithValue("b_id", model.BenefitId);
            cmd.Parameters.AddWithValue("s_s_id", model.SocialStatusId);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT currval('abonents_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class BenefitRepository : AbstractRepository<BenefitModel>, IBenefitRepository
    {
        public BenefitRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT b.id AS id, benefit_type_id, bt.name AS benefit_type_name, conditions, tariff " +
            "FROM benefits b " +
            "JOIN benefit_types bt ON bt.id = b.benefit_type_id " +
            "ORDER BY b.id";

        protected override async Task OnCreate(BenefitModel model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO benefits (benefit_type_id, conditions, tariff) VALUES (@b_id, @cond, @tariff)", Connection);

            cmd.Parameters.AddWithValue("b_id", model.BenefitTypeId);
            cmd.Parameters.AddWithValue("cond", model.Conditions);
            cmd.Parameters.AddWithValue("tariff", model.Tariff);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('benefits_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM benefits WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(BenefitModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE benefits SET tariff = @tariff, conditions = @conditions, benefit_type_id = @benefit_type_id " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("tariff", model.Tariff);
            cmd.Parameters.AddWithValue("conditions", model.Conditions);
            cmd.Parameters.AddWithValue("benefit_type_id", model.BenefitTypeId);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

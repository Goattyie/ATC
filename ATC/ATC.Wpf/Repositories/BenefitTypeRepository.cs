using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class BenefitTypeRepository : AbstractRepository<BenefitTypeModel>, IBenefitTypeRepository
    {
        public BenefitTypeRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT * FROM benefit_types";

        protected override async Task OnCreate(BenefitTypeModel model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO benefit_types (name) VALUES (@name);", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('benefit_types_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM benefit_types WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(BenefitTypeModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE benefit_types SET name = @name " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("name", model.Name);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

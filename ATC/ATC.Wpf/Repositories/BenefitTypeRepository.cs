using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class BenefitTypeRepository : AbstractRepository<BenefitType>, IBenefitTypeRepository
    {
        public BenefitTypeRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => throw new System.NotImplementedException();

        protected override async Task OnCreate(BenefitType model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO benefit_types (name) VALUES (@name);", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('benefit_types_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override Task OnDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnUpdate(BenefitType model)
        {
            throw new System.NotImplementedException();
        }
    }
}

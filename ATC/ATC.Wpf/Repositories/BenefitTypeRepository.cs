using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class BenefitTypeRepository : AbstractRepository<BenefitType>, IBenefitTypeRepository
    {
        public BenefitTypeRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(BenefitType model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO benefit_types (name) VALUES (@name)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT id FROM benefit_types WHERE name = @name;", Connection);

            cmd2.Parameters.AddWithValue("name", model.Name);

            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

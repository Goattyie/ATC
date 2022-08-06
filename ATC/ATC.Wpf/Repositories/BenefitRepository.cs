using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class BenefitRepository : AbstractRepository<Benefit>, IBenefitRepository
    {
        public BenefitRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Benefit model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO benefits (benefit_type_id, conditions, tariff, photo) VALUES (@b_id, @cond, @tariff, @photo)", Connection);

            cmd.Parameters.AddWithValue("b_id", model.BenefitTypeId);
            cmd.Parameters.AddWithValue("cond", model.Conditions);
            cmd.Parameters.AddWithValue("tariff", model.Tariff);
            cmd.Parameters.AddWithValue("photo", model.Photo);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('benefits_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class SocialStatusRepository : AbstractRepository<SocialStatus>, ISocialStatusRepository
    {
        public SocialStatusRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(SocialStatus model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO social_statuses (name) VALUES (@name)", Connection);

            cmd.Parameters.AddWithValue("name", model.Name);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('social_statuses_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

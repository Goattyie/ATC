using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AtcRepository : AbstractRepository<Atc>, IAtcRepository
    {
        public AtcRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Atc model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO atces (code, name, area_id, address, open_year, is_state, license) " +
                "VALUES (@code, @name, @a_id, @address, @open_year, @is_state, @license)", Connection);

            cmd.Parameters.AddWithValue("code", model.Code);
            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("a_id", model.AreaId);
            cmd.Parameters.AddWithValue("address", model.Address);
            cmd.Parameters.AddWithValue("open_year", model.OpenYear);
            cmd.Parameters.AddWithValue("is_state", model.IsState);
            cmd.Parameters.AddWithValue("license", model.License);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand("SELECT currval('atces_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }
    }
}

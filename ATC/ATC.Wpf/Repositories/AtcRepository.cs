using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class AtcRepository : AbstractRepository<AtcModel>, IAtcRepository
    {
        public AtcRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT atces.id as id, code, atces.name, address, areas.id as area_id, areas.name as area_name, open_year, is_state, license " +
            "FROM atces " +
            "JOIN areas ON areas.id = area_id ORDER BY id ASC;";

        protected override async Task OnCreate(AtcModel model)
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

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM atces WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(AtcModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE atces SET code = @code, name = @name, address = @address, open_year = @open_year, area_id = @a_id, is_state = @is_state, license = @license " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("code", model.Code);
            cmd.Parameters.AddWithValue("name", model.Name);
            cmd.Parameters.AddWithValue("a_id", model.AreaId);
            cmd.Parameters.AddWithValue("address", model.Address);
            cmd.Parameters.AddWithValue("open_year", model.OpenYear);
            cmd.Parameters.AddWithValue("is_state", model.IsState);
            cmd.Parameters.AddWithValue("license", model.License);
            
            await cmd.ExecuteNonQueryAsync();
        }
    }
}

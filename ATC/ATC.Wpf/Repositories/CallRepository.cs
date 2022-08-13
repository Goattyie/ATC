using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class CallRepository : AbstractRepository<CallModel>, ICallRepository
    {
        public CallRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string SelectQuery => "SELECT call.id AS id, atc.name AS atc_name, atc_id AS atc_id, city.id AS city_id, city.name AS city_name, cost, call.phone AS phone, call_date, call.time, tariff_id AS tariff_id, tariff.name AS tariff_name, abonent_id, abonent.phone AS abonent_phone " +
            "FROM calls call " +
            "JOIN atces atc ON atc.id = call.atc_id " +
            "JOIN tariffs tariff ON tariff.id = call.tariff_id " +
            "JOIN abonents abonent ON abonent.id = call.abonent_id " +
            "JOIN cities city ON city.id = call.city_id " +
            "ORDER BY call.id";

        protected override async Task OnCreate(CallModel model)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO calls (atc_id, city_id, cost, phone, call_date, time, tariff_id, abonent_id) " +
                "VALUES (@atc_id, @city_id, @cost, @phone, @call_date, @time, @tariff_id, @abonent_id)", Connection);

            cmd.Parameters.AddWithValue("atc_id", model.AtcId);
            cmd.Parameters.AddWithValue("city_id", model.CityId);
            cmd.Parameters.AddWithValue("cost", model.Cost);
            cmd.Parameters.AddWithValue("phone", model.Phone);
            cmd.Parameters.AddWithValue("call_date", model.CallDate);
            cmd.Parameters.AddWithValue("time", model.Time);
            cmd.Parameters.AddWithValue("tariff_id", model.TariffId);
            cmd.Parameters.AddWithValue("abonent_id", model.AbonentId);
            await cmd.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand($"SELECT currval('calls_id_seq');", Connection);
            await using var reader = await cmd2.ExecuteReaderAsync();

            await reader.ReadAsync();

            model.Id = reader.GetInt32(0);
        }

        protected override async Task OnDelete(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM calls WHERE id = @id ", Connection);

            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        protected override async Task OnUpdate(CallModel model)
        {
            await using var cmd = new NpgsqlCommand("UPDATE calls SET atc_id = @atc_id, city_id = @city_id, cost = @cost, phone = @phone, call_date = @call_date, time = @time, tariff_id = @tariff_id, abonent_id = @abonent_id " +
                "WHERE id = @id; ", Connection);

            cmd.Parameters.AddWithValue("id", model.Id);
            cmd.Parameters.AddWithValue("atc_id", model.AtcId);
            cmd.Parameters.AddWithValue("city_id", model.CityId);
            cmd.Parameters.AddWithValue("cost", model.Cost);
            cmd.Parameters.AddWithValue("phone", model.Phone);
            cmd.Parameters.AddWithValue("call_date", model.CallDate);
            cmd.Parameters.AddWithValue("time", model.Time);
            cmd.Parameters.AddWithValue("tariff_id", model.TariffId);
            cmd.Parameters.AddWithValue("abonent_id", model.AbonentId);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

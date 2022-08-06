using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal class CallRepository : AbstractRepository<Call>, ICallRepository
    {
        public CallRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task OnCreate(Call model)
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
    }
}

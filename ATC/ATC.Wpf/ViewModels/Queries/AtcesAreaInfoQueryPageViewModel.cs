using ATC.Wpf.DI;
using ATC.Wpf.Queries.Interfaces;
using DevExpress.Mvvm;
using Npgsql;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class AtcesAreaInfoQueryPageViewModel : AbstractQueryPageViewModel<BaseInput, AtcesAreaInfoResult>
    {
        private readonly NpgsqlConnection _connection;

        public AtcesAreaInfoQueryPageViewModel(IAtcesAreaInfoQuery query, NpgsqlConnection connection) : base(query)
        {
            _connection = connection;
        }

        public AtcesAreaInfoResult SelectedAtcesAreaInfo { get; set; }
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO abonents (first_name, second_name, last_name, phone, benefit_id, social_status_id, photo) " +
                "VALUES (@f_n, @s_n, @l_n, @phone, @b_id, @s_s_id, @photo)", _connection);

            await cmd.ExecuteNonQueryAsync();
        });
    }
}

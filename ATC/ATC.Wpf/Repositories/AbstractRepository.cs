using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal abstract class AbstractRepository<TModel> : IRepository<TModel> where TModel : BaseModel
    {
        protected NpgsqlConnection Connection { get; private set; }

        protected AbstractRepository(NpgsqlConnection connection)
        {
            Connection = connection;
        }

        protected abstract Task OnCreate(TModel model);
        public async Task Create(TModel model)
        {
            await Connection.OpenAsync();
            await OnCreate(model);
            await Connection.CloseAsync();
        }
    }
}

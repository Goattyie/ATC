using ATC.Wpf.Extensions;
using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories
{
    internal abstract class AbstractRepository<TModel> : IRepository<TModel> where TModel : BaseModel
    {
        protected NpgsqlConnection Connection { get; private set; }

        protected virtual PropertyInfo[] IncludableProperties { get; }

        protected AbstractRepository(NpgsqlConnection connection)
        {
            Connection = connection;
        }

        protected abstract string SelectQuery { get; }
        protected abstract Task OnCreate(TModel model);
        protected abstract Task OnUpdate(TModel model);
        protected abstract Task OnDelete(int id);
        public async Task Create(TModel model)
        {
            await Connection.OpenAsync();
            await OnCreate(model);
            await Connection.CloseAsync();
        }

        public async Task Update(TModel model)
        {
            await Connection.OpenAsync();
            await OnUpdate(model);
            await Connection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await Connection.OpenAsync();
            await OnDelete(id);
            await Connection.CloseAsync();
        }

        public async Task<IEnumerable<TModel>> Get()
        {
            await Connection.OpenAsync();

            await using var cmd = new NpgsqlCommand(SelectQuery, Connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            var list = await reader.ReadToList<TModel>();

            await Connection.CloseAsync();

            return list;
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.CloseAsync();
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using Npgsql;
using System.Threading.Tasks;

namespace ATC.Wpf.Services.DataGenerators
{
    internal abstract class AbstractGenerator<TModel>
        where TModel : BaseModel
    {
        protected IRepository<TModel> Repository { get; private set; }
        protected NpgsqlConnection Connection { get; private set; }
        
        public AbstractGenerator(IRepository<TModel> repository, NpgsqlConnection connection)
        {
            Repository = repository;
            Connection = connection;
        }

        public async Task Generate()
        {
            await Clear();
            await GenerateData();
        }

        protected abstract Task Clear();
        protected abstract Task GenerateData();
    }
}

using ATC.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories.Interfaces
{
    internal interface IRepository<TModel> : IAsyncDisposable 
        where TModel : BaseModel
    {
        Task<IEnumerable<TModel>> Get();
        Task Create(TModel model);
        Task Update(TModel model);
        Task Delete(int id);
    }
}
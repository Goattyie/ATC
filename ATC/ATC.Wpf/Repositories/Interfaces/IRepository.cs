using ATC.Wpf.Models;
using System.Threading.Tasks;

namespace ATC.Wpf.Repositories.Interfaces
{
    internal interface IRepository<TModel> where TModel : BaseModel
    {
        Task Create(TModel model);
    }
}
using ATC.Wpf.Queries.Interfaces;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace ATC.Wpf.ViewModels.Queries
{
    internal abstract class AbstractQueryPageViewModel<TInput, TOutput> : BindableBase
        where TInput : BaseInput, new()
    {
        protected IQuery<TInput, TOutput> Query { get; }

        public AbstractQueryPageViewModel(IQuery<TInput, TOutput> query)
        {
            Query = query;
            Data = new();
            InputData = new();
        }

        public TInput InputData { get; set; }
        public ObservableCollection<TOutput> Data { get; set; }

        public IAsyncCommand ExecuteCommand => new AsyncCommand(async () =>
        {
            Data.Clear();

            var result = await Query.ExecuteQuery(InputData);

            foreach (var item in result)
                Data.Add(item);
        });
    }
}

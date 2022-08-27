using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables
{
    internal class AbstractModelMessage<TModel> : IMessage
        where TModel : BaseModel
    {
        public TModel Model { get; set; }
    }

    internal class AbstractNewModelEvent<TModel> : IEvent
        where TModel : BaseModel
    {
        public TModel Model { get; set; }
    }

    internal class AbstractUpdateModelEvent<TModel> : IEvent
        where TModel : BaseModel
    {
        public TModel Model { get; set; }
    }

    internal abstract class AbstractTablePageViewModel<TCreateWindow, TUpdateWindow, TModel, TUpdateWindowViewModel> : BindableBase
        where TCreateWindow : Window, new()
        where TUpdateWindow : Window, new()
        where TModel : BaseModel
        where TUpdateWindowViewModel : BindableBase
    {
        protected IRepository<TModel> Repository { get; }
        protected MessageBus MessageBus { get; }

        public AbstractTablePageViewModel(IRepository<TModel> repository, MessageBus messageBus, EventBus eventBus)
        {
            Repository = repository;
            MessageBus = messageBus;

            Data = new();

            LoadData();

            eventBus.Subscribe<AbstractNewModelEvent<TModel>>((@event) => { Data.Add(@event.Model); return Task.CompletedTask; });
            eventBus.Subscribe<AbstractUpdateModelEvent<TModel>>((@event) =>
            {
                var oldValue = Data.First(x => x.Id == @event.Model.Id);
                var index = Data.IndexOf(oldValue);

                Data.Remove(oldValue);
                Data.Insert(index, @event.Model);

                return Task.CompletedTask;
            });

            DataInitializer.OnGenerate += async () => await LoadData();

        }

        protected abstract IEnumerable<TModel> FilterData(IEnumerable<TModel> allData);

        public abstract List<string> Filters { get; set; }
        public string SelectedFilter { get; set; }
        public string FilterValue { get; set; }
        public ObservableCollection<TModel> Data { get; set; }
        public TModel SelectedData { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new TCreateWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedData is null)
                return;

            new TUpdateWindow().Show();

            await MessageBus.SendTo<TUpdateWindowViewModel>(new AbstractModelMessage<TModel> { Model = SelectedData });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedData != null)
            {
                await Repository.Delete(SelectedData.Id);
                Data.Remove(SelectedData);
            }
        });
        public IAsyncCommand FilterCommand => new AsyncCommand(async () =>
        {
            var data = await Repository.Get();

            data = FilterData(data);

            LoadData(data);
        });
        protected async Task LoadData()
        {
            Data.Clear();

            var data = await Repository.Get();

            foreach (var item in data)
                Data.Add(item);
        }

        protected void LoadData(IEnumerable<TModel> list)
        {
            Data.Clear();

            foreach (var item in list)
                Data.Add(item);
        }
    }
}

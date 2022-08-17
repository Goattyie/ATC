using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Area;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Area
{
    internal class AreaModelMessage : IMessage
    {
        public AreaModel Area { get; set; }
    }

    internal class AreaNewModelEvent : IEvent
    {
        public AreaModel Area { get; set; }
    }

    internal class AreaUpdateModelEvent : IEvent
    {
        public AreaModel Area { get; set; }
    }

    internal class AreaPageViewModel : BindableBase
    {
        private readonly IAreaRepository _repository;
        private readonly MessageBus _messageBus;

        public AreaPageViewModel(IAreaRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Areas = new ObservableCollection<AreaModel>();

            LoadData();

            eventBus.Subscribe<AreaNewModelEvent>((@event) => { Areas.Add(@event.Area); return Task.CompletedTask; });
            eventBus.Subscribe<AreaUpdateModelEvent>((@event) =>
            {
                var oldValue = Areas.First(x => x.Id == @event.Area.Id);
                var index = Areas.IndexOf(oldValue);

                Areas.Remove(oldValue);
                Areas.Insert(index, @event.Area);

                return Task.CompletedTask;
            });
        }

        public ObservableCollection<AreaModel> Areas { get; set; }
        public AreaModel SelectedArea { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateAreaWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedArea is null)
                return;

            new UpdateAreaWindow().Show();

            await _messageBus.SendTo<UpdateAreaWindowViewModel>(new AreaModelMessage { Area = SelectedArea });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedArea != null)
            {
                await _repository.Delete(SelectedArea.Id);
                Areas.Remove(SelectedArea);
            }
        });

        private async Task LoadData()
        {
            Areas.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Areas.Add(item);
        }
    }
}

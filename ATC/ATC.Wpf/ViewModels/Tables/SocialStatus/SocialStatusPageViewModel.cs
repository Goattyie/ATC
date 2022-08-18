using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.SocialStatus;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.SocialStatus
{
    internal class SocialStatusModelMessage : IMessage
    {
        public SocialStatusModel SocialStatus { get; set; }
    }

    internal class SocialStatusNewModelEvent : IEvent
    {
        public SocialStatusModel SocialStatus { get; set; }
    }

    internal class SocialStatusUpdateModelEvent : IEvent
    {
        public SocialStatusModel SocialStatus { get; set; }
    }

    internal class SocialStatusPageViewModel : BindableBase
    {
        private readonly ISocialStatusRepository _repository;
        private readonly MessageBus _messageBus;

        public ObservableCollection<SocialStatusModel> SocialStatuses { get; }
        public SocialStatusModel SelectedSocialStatus { get; set; }

        public SocialStatusPageViewModel(ISocialStatusRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            SocialStatuses = new ObservableCollection<SocialStatusModel>();

            LoadData();

            eventBus.Subscribe<SocialStatusNewModelEvent>((@event) => { SocialStatuses.Add(@event.SocialStatus); return Task.CompletedTask; });
            eventBus.Subscribe<SocialStatusUpdateModelEvent>((@event) =>
            {
                var oldValue = SocialStatuses.First(x => x.Id == @event.SocialStatus.Id);
                var index = SocialStatuses.IndexOf(oldValue);

                SocialStatuses.Remove(oldValue);
                SocialStatuses.Insert(index, @event.SocialStatus);

                return Task.CompletedTask;
            });
        }

        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateSocialStatusWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedSocialStatus is null)
                return;

            new UpdateSocialStatusWindow().Show();

            await _messageBus.SendTo<UpdateSocialStatusWindowViewModel>(new SocialStatusModelMessage { SocialStatus = SelectedSocialStatus });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedSocialStatus != null)
            {
                await _repository.Delete(SelectedSocialStatus.Id);
                SocialStatuses.Remove(SelectedSocialStatus);
            }
        });

        private async Task LoadData()
        {
            SocialStatuses.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                SocialStatuses.Add(item);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.BenefitType;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.BenefitType
{
    internal class BenefitTypeMessage : IMessage
    {
        public BenefitTypeModel BenefitType { get; set; }
    }

    internal class BenefitTypeNewModelEvent : IEvent
    {
        public BenefitTypeModel BenefitType { get; set; }
    }

    internal class BenefitTypeUpdateModelEvent : IEvent
    {
        public BenefitTypeModel BenefitType { get; set; }
    }

    internal class BenefitTypePageViewModel : BindableBase
    {
        private readonly IBenefitTypeRepository _repository;
        private readonly MessageBus _messageBus;

        public BenefitTypePageViewModel(IBenefitTypeRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            BenefitTypes = new ObservableCollection<BenefitTypeModel>();

            LoadData();

            eventBus.Subscribe<BenefitTypeNewModelEvent>((@event) => { BenefitTypes.Add(@event.BenefitType); return Task.CompletedTask; });
            eventBus.Subscribe<BenefitTypeUpdateModelEvent>((@event) =>
            {
                var oldValue = BenefitTypes.First(x => x.Id == @event.BenefitType.Id);
                var index = BenefitTypes.IndexOf(oldValue);

                BenefitTypes.Remove(oldValue);
                BenefitTypes.Insert(index, @event.BenefitType);

                return Task.CompletedTask;
            });
        }

        public new ObservableCollection<BenefitTypeModel> BenefitTypes { get; set; }
        public BenefitTypeModel SelectedBenefitType { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateBenefitTypeWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedBenefitType is null)
                return;

            new UpdateBenefitTypeWindow().Show();

            await _messageBus.SendTo<UpdateBenefitTypeWindowViewModel>(new BenefitTypeMessage { BenefitType = SelectedBenefitType });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedBenefitType != null)
            {
                await _repository.Delete(SelectedBenefitType.Id);
                BenefitTypes.Remove(SelectedBenefitType);
            }
        });

        private async Task LoadData()
        {
            BenefitTypes.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                BenefitTypes.Add(item);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Benefit;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Benefit
{
    internal class BenefitModelMessage : IMessage
    {
        public BenefitModel Benefit { get; set; }
    }

    internal class BenefitNewModelEvent : IEvent
    {
        public BenefitModel Benefit { get; set; }
    }

    internal class BenefitUpdateModelEvent : IEvent
    {
        public BenefitModel Benefit { get; set; }
    }

    internal class BenefitPageViewModel : BindableBase
    {
        private readonly IBenefitRepository _repository;
        private readonly MessageBus _messageBus;

        public BenefitPageViewModel(IBenefitRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Benefits = new ObservableCollection<BenefitModel>();

            LoadData();

            eventBus.Subscribe<BenefitNewModelEvent>((@event) => { Benefits.Add(@event.Benefit); return Task.CompletedTask; });
            eventBus.Subscribe<BenefitUpdateModelEvent>((@event) =>
            {
                var oldValue = Benefits.First(x => x.Id == @event.Benefit.Id);
                var index = Benefits.IndexOf(oldValue);

                Benefits.Remove(oldValue);
                Benefits.Insert(index, @event.Benefit);

                return Task.CompletedTask;
            });
        }

        public new ObservableCollection<BenefitModel> Benefits { get; set; }
        public BenefitModel SelectedBenefit { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateBenefitPage().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedBenefit is null)
                return;

            new UpdateBenefitWindow().Show();

            await _messageBus.SendTo<UpdateBenefitWindowViewModel>(new BenefitModelMessage { Benefit = SelectedBenefit });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedBenefit != null)
            {
                await _repository.Delete(SelectedBenefit.Id);
                Benefits.Remove(SelectedBenefit);
            }
        });

        private async Task LoadData()
        {
            Benefits.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Benefits.Add(item);
        }
    }
}

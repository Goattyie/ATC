using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Atc;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Atc
{
    internal class AtcModelMessage : IMessage
    {
        public AtcModel Atc { get; set; }
    }

    internal class AtcNewModelEvent : IEvent
    {
        public AtcModel Atc { get; set; }
    }

    internal class AtcUpdateModelEvent : IEvent
    {
        public AtcModel Atc { get; set; }
    }

    internal class AtcPageViewModel : BindableBase
    {
        private readonly IAtcRepository _repository;
        private readonly MessageBus _messageBus;

        public AtcPageViewModel(IAtcRepository repository, MessageBus messageBus, EventBus eventBus)
        {
            _repository = repository;
            _messageBus = messageBus;

            Atces = new ObservableCollection<AtcModel>();

            LoadData();

            eventBus.Subscribe<AtcNewModelEvent>((@event) => { Atces.Add(@event.Atc); return Task.CompletedTask; });
            eventBus.Subscribe<AtcUpdateModelEvent>((@event) => 
            { 
                var oldValue = Atces.First(x => x.Id == @event.Atc.Id);
                var index = Atces.IndexOf(oldValue);
                
                Atces.Remove(oldValue); 
                Atces.Insert(index, @event.Atc); 
                
                return Task.CompletedTask; 
            });
        }

        public Action CloseWindow { get; set; }
        public ObservableCollection<AtcModel> Atces { get; set; }
        public AtcModel SelectedAtc { get; set; }
        public IDelegateCommand CreateCommand => new DelegateCommand(() => { new CreateAtcWindow().Show(); });
        public IAsyncCommand UpdateCommand => new AsyncCommand(async () =>
        {
            if (SelectedAtc is null)
                return;

            new UpdateAtcWindow().Show();

            await _messageBus.SendTo<UpdateAtcWindowViewModel>(new AtcModelMessage { Atc = SelectedAtc });
        });
        public IAsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            while (SelectedAtc != null)
            {
                await _repository.Delete(SelectedAtc.Id);
                Atces.Remove(SelectedAtc);
            }
        });

        private async Task LoadData()
        {
            Atces.Clear();

            var list = await _repository.Get();

            foreach (var item in list)
                Atces.Add(item);
        }
    }
}

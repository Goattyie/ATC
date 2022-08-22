using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Benefit
{
    internal class UpdateBenefitWindowViewModel : BindableBase
    {
        private readonly IBenefitTypeRepository _benefitTypeRepository;
        private readonly IBenefitRepository _benefitRepository;
        private readonly EventBus _eventBus;

        public UpdateBenefitWindowViewModel(IBenefitRepository benefitRepository,
            IBenefitTypeRepository benefitTypeRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _benefitTypeRepository = benefitTypeRepository;
            _benefitRepository = benefitRepository;
            _eventBus = eventBus;

            Benefit = new();
            BenefitTypes = new();

            messageBus.Recieve<AbstractModelMessage<BenefitModel>>(this, LoadData);
        }

        public BenefitModel Benefit { get; set; }
        public ObservableCollection<BenefitTypeModel> BenefitTypes { get; set; }
        public BenefitTypeModel SelectedBenefitType { get; set; }
        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Benefit.BenefitTypeId = SelectedBenefitType?.Id ?? 0;
            Benefit.BenetitTypeName = SelectedBenefitType?.Name;

            try
            {
                await _benefitRepository.Update(Benefit);
                await _eventBus.Publish(new AbstractUpdateModelEvent<BenefitModel> { Model = Benefit });
                window.Close();
                MessageBoxManager.ShowInformation("Льгота успешно обновлена.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _benefitRepository.DisposeAsync();
            }
        });

        private async Task LoadData(AbstractModelMessage<BenefitModel> msg)
        {
            Benefit = msg.Model;

            BenefitTypes.Clear();

            var benefitTypes = await _benefitTypeRepository.Get();

            foreach (var item in benefitTypes)
                BenefitTypes.Add(item);

            SelectedBenefitType = BenefitTypes.FirstOrDefault(x => x.Id == Benefit.BenefitTypeId);
        }
    }
}
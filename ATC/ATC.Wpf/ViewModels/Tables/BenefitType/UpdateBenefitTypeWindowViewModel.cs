using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.BenefitType
{
    internal class UpdateBenefitTypeWindowViewModel : BindableBase
    {
        private readonly IBenefitTypeRepository _benefitTypeRepository;
        private readonly EventBus _eventBus;

        public UpdateBenefitTypeWindowViewModel(IBenefitTypeRepository benefitRepository,
            IBenefitTypeRepository benefitTypeRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _benefitTypeRepository = benefitTypeRepository;
            _eventBus = eventBus;

            BenefitType = new();

            messageBus.Recieve<BenefitTypeMessage>(this, (msg) =>
            {
                BenefitType = msg.BenefitType;
                return Task.CompletedTask;
            });
        }

        public BenefitTypeModel BenefitType { get; set; }
        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _benefitTypeRepository.Update(BenefitType);
                await _eventBus.Publish(new BenefitTypeUpdateModelEvent { BenefitType = BenefitType });
                window.Close();
                MessageBoxManager.ShowInformation("Льгота успешно обновлена.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _benefitTypeRepository.DisposeAsync();
            }
        });
    }
}

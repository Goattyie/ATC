using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.BenefitType
{
    internal class CreateBenefitTypeWindowViewModel : BindableBase
    {
        private readonly IBenefitTypeRepository _benefitTypeRepository;
        private readonly EventBus _eventBus;

        public CreateBenefitTypeWindowViewModel(IBenefitTypeRepository benefitRepository,
            IBenefitTypeRepository benefitTypeRepository,
            EventBus eventBus)
        {
            _benefitTypeRepository = benefitTypeRepository;
            _eventBus = eventBus;

            BenefitType = new();
        }

        public BenefitTypeModel BenefitType { get; set; }
        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _benefitTypeRepository.Create(BenefitType);
                await _eventBus.Publish(new AbstractNewModelEvent<BenefitTypeModel> { Model = BenefitType });
                window.Close();
                MessageBoxManager.ShowInformation("Льгота успешно добавлена.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _benefitTypeRepository.DisposeAsync();
            }
        });
    }
}

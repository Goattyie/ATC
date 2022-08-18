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
    internal class CreateBenefitWindowViewModel : BindableBase
    {
        private readonly IBenefitTypeRepository _benefitTypeRepository;
        private readonly IBenefitRepository _benefitRepository;
        private readonly EventBus _eventBus;

        public CreateBenefitWindowViewModel(IBenefitRepository benefitRepository,
            IBenefitTypeRepository benefitTypeRepository,
            EventBus eventBus)
        {
            _benefitTypeRepository = benefitTypeRepository;
            _benefitRepository = benefitRepository;
            _eventBus = eventBus;

            Benefit = new();
            BenefitTypes = new();

            LoadData();
        }

        public BenefitModel Benefit { get; set; }
        public ObservableCollection<BenefitTypeModel> BenefitTypes { get; set; }
        public BenefitTypeModel SelectedBenefitType { get; set; }
        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Benefit.BenefitTypeId = SelectedBenefitType?.Id ?? 0;
            Benefit.BenetitTypeName = SelectedBenefitType?.Name;

            try
            {
                await _benefitRepository.Create(Benefit);
                await _eventBus.Publish(new BenefitNewModelEvent { Benefit = Benefit });
                window.Close();
                MessageBoxManager.ShowInformation("Льгота успешно добавлена.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _benefitRepository.DisposeAsync();
            }
        });

        private async Task LoadData()
        {
            BenefitTypes.Clear();

            var benefitTypes = await _benefitTypeRepository.Get();

            foreach (var item in benefitTypes)
                BenefitTypes.Add(item);

            SelectedBenefitType = BenefitTypes.FirstOrDefault();
        }
    }
}

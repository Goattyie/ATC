using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Benefit;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Benefit
{
    internal class BenefitPageViewModel : AbstractTablePageViewModel<CreateBenefitPage, UpdateBenefitWindow, BenefitModel, UpdateBenefitWindowViewModel>
    {
        public BenefitPageViewModel(IBenefitRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Тип льготы",
            "Условия",
            "Тариф"
        };

        protected override IEnumerable<BenefitModel> FilterData(IEnumerable<BenefitModel> allData) => SelectedFilter switch
        {
            "Тип льготы" => allData.Where(x => x.BenetitTypeName.Contains(FilterValue)),
            "Условия" => allData.Where(x => x.Conditions.Contains(FilterValue)),
            "Тариф" => allData.Where(x => x.Tariff.Contains(FilterValue)),
            _ => allData
        };
    }
}

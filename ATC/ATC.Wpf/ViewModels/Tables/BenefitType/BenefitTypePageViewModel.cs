using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.BenefitType;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.BenefitType
{
    internal class BenefitTypePageViewModel : AbstractTablePageViewModel<CreateBenefitTypeWindow, UpdateBenefitTypeWindow, BenefitTypeModel, UpdateBenefitTypeWindowViewModel>
    {
        public BenefitTypePageViewModel(IBenefitTypeRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Название"
        };

        protected override IEnumerable<BenefitTypeModel> FilterData(IEnumerable<BenefitTypeModel> allData) => SelectedFilter switch
        {
            "Название" => allData.Where(x => x.Name.Contains(FilterValue)),
            _ => allData
        };
    }
}

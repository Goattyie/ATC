using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.SocialStatus;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.SocialStatus
{
    internal class SocialStatusPageViewModel : AbstractTablePageViewModel<CreateSocialStatusWindow, UpdateSocialStatusWindow, SocialStatusModel, UpdateSocialStatusWindowViewModel>
    {
        public SocialStatusPageViewModel(ISocialStatusRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string> { "Название" };

        protected override IEnumerable<SocialStatusModel> FilterData(IEnumerable<SocialStatusModel> allData) => SelectedFilter switch
        {
            "Название" => allData.Where(x => x.Name.Contains(FilterValue)),
            _ => allData
        };
    }
}

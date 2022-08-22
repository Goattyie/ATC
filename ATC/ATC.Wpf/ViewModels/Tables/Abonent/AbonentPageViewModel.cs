using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Abonent;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATC.Wpf.ViewModels.Tables.Abonent
{
    internal class AbonentPageViewModel : AbstractTablePageViewModel<CreateAbonentWindow, UpdateAbonentWindow, AbonentModel, UpdateAbonentWindowViewModel>
    {
        public AbonentPageViewModel(IAbonentRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Фамилия",
            "Имя",
            "Отчество",
            "Номер телефона",
            "Фото",
            "Социальный статус"
        };

        protected override IEnumerable<AbonentModel> FilterData(IEnumerable<AbonentModel> allData) => SelectedFilter switch
        {
            "Фамилия" => allData.Where(x => x.SecondName.Contains(FilterValue)),
            "Имя" => allData.Where(x => x.FirstName.Contains(FilterValue)),
            "Отчество" => allData.Where(x => x.LastName.Contains(FilterValue)),
            "Номер телефона" => allData.Where(x => x.Phone.Contains(FilterValue)),
            "Фото" => allData.Where(x => x.Photo.Contains(FilterValue)),
            "Социальный статус" => allData.Where(x => x.SocialStatusName.Contains(FilterValue))
        };
    }
}

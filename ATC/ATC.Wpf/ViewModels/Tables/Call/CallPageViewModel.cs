using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Views.Tables.Call;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Wpf.ViewModels.Tables.Call
{
    internal class CallPageViewModel : AbstractTablePageViewModel<CreateCallWindow, UpdateCallWindow, CallModel, UpdateCallWindowViewModel>
    {
        public CallPageViewModel(ICallRepository repository, MessageBus messageBus, EventBus eventBus) : base(repository, messageBus, eventBus)
        {
        }

        public override List<string> Filters { get; set; } = new List<string>
        {
            "Название АТС",
            "Город",
            "Цена",
            "Куда звонили",
            "Откуда звонили",
            "Время разговора",
            "Дата звонка",
            "Название тарифа"
        };

        protected override IEnumerable<CallModel> FilterData(IEnumerable<CallModel> allData) => SelectedFilter switch
        {
            "Название АТС" => allData.Where(x => x.AtcName.Contains(FilterValue)),
            "Город" => allData.Where(x => x.CityName.Contains(FilterValue)),
            "Цена" => allData.Where(x => x.Cost.ToString().Contains(FilterValue)),
            "Куда звонили" => allData.Where(x => x.Phone.Contains(FilterValue)),
            "Откуда звонили" => allData.Where(x => x.AbonentPhone.Contains(FilterValue)),
            "Время разговора" => allData.Where(x => x.Time.ToString().Contains(FilterValue)),
            "Дата звонка" => allData.Where(x => x.CallDate.ToString("dd.MM.yyyy").Contains(FilterValue)),
            "Название тарифа" => allData.Where(x => x.TariffName.Contains(FilterValue)),
        };
    }
}

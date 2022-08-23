using ATC.Wpf.Abstractions;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Services.DataGenerators;
using ATC.Wpf.Views.Queries;
using ATC.Wpf.Views.Tables;
using ATC.Wpf.Views.Tables.Abonent;
using ATC.Wpf.Views.Tables.Area;
using ATC.Wpf.Views.Tables.Benefit;
using ATC.Wpf.Views.Tables.BenefitType;
using ATC.Wpf.Views.Tables.Call;
using ATC.Wpf.Views.Tables.City;
using ATC.Wpf.Views.Tables.Country;
using ATC.Wpf.Views.Tables.SocialStatus;
using ATC.Wpf.Views.Tables.Tariff;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ATC.Wpf.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly DataInitializer _dataInitializer;
        private readonly IEnumerable<ITablePage> _tablePages;
        private readonly QueriesControlPage _queryPage;

        public MainWindowViewModel(IEnumerable<ITablePage> tablePages, QueriesControlPage queryPage, DataInitializer dataInitializer)
        {
            _dataInitializer = dataInitializer;
            _tablePages = tablePages;
            _queryPage = queryPage;
            CurrentPage = (Page)_tablePages.FirstOrDefault();
        }

        public Page CurrentPage { get; set; }

        public IDelegateCommand SelectTablePage => new DelegateCommand<string>((string table) =>
        {
            switch (table)
            {
                case "atc": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(AtcPage)); break;
                case "call": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(CallPage)); break;
                case "abonent": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(AbonentPage)); break;
                case "benefit": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(BenefitPage)); break;
                case "country": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(CountryPage)); break;
                case "city": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(CityPage)); break;
                case "area": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(AreaPage)); break;
                case "tariff": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(TariffPage)); break;
                case "socialstatus": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(SocialStatusPage)); break;
                case "benefittype": CurrentPage = (Page)_tablePages.First(x => x.GetType() == typeof(BenefitTypePage)); break;
            }
        });

        public IAsyncCommand GenerateData => new AsyncCommand(async () =>
        {
            try { await _dataInitializer.Generate(); }
            catch(Exception ex) 
            {
                MessageBoxManager.ShowError(ex.Message);
            }
        });

        public IDelegateCommand QueryModule => new DelegateCommand(() =>
        {
            CurrentPage = _queryPage;
        });
    }
}

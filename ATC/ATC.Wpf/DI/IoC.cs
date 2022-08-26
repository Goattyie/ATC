using ATC.Wpf.Abstractions;
using ATC.Wpf.Queries;
using ATC.Wpf.Queries.Interfaces;
using ATC.Wpf.Repositories;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Services.DataGenerators;
using ATC.Wpf.ViewModels;
using ATC.Wpf.ViewModels.Queries;
using ATC.Wpf.ViewModels.Tables.Abonent;
using ATC.Wpf.ViewModels.Tables.Area;
using ATC.Wpf.ViewModels.Tables.Atc;
using ATC.Wpf.ViewModels.Tables.Benefit;
using ATC.Wpf.ViewModels.Tables.BenefitType;
using ATC.Wpf.ViewModels.Tables.Call;
using ATC.Wpf.ViewModels.Tables.City;
using ATC.Wpf.ViewModels.Tables.Country;
using ATC.Wpf.ViewModels.Tables.SocialStatus;
using ATC.Wpf.ViewModels.Tables.Tariff;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;

namespace ATC.Wpf.DI
{
    internal static class IoC
    {
        private static readonly IServiceProvider _serviceProvider;

        static IoC()
        {
            var services = new ServiceCollection();

            #region Database

            services.AddTransient(o => new NpgsqlConnection("server=127.0.0.1;port=5432;database=ATCDB;user id=postgres;password=9156;Pooling=true;Timeout=60;Command Timeout=60;"));

            #endregion

            #region Repositories

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<ITariffRepository, TariffRepository>();
            services.AddTransient<IBenefitTypeRepository, BenefitTypeRepository>();
            services.AddTransient<ISocialStatusRepository, SocialStatusRepository>();
            services.AddTransient<IBenefitRepository, BenefitRepository>();
            services.AddTransient<IAbonentRepository, AbonentRepository>();
            services.AddTransient<IAtcRepository, AtcRepository>();
            services.AddTransient<ICallRepository, CallRepository>();

            #endregion

            #region Data Generators

            services.AddTransient<CityGenerator>();
            services.AddTransient<CountryGenerator>();
            services.AddTransient<AreaGenerator>();
            services.AddTransient<TariffGenerator>();
            services.AddTransient<BenefitTypeGenerator>();
            services.AddTransient<SocialStatusGenerator>();
            services.AddTransient<BenefitGenerator>();
            services.AddTransient<AbonentGenerator>();
            services.AddTransient<AtcGenerator>();
            services.AddTransient<CallGenerator>();
            services.AddTransient<DataInitializer>();

            #endregion

            #region ViewModels

            services.AddTransient<SignInWindowViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AtcPageViewModel>();
            services.AddTransient<CreateAtcWindowViewModel>();
            services.AddTransient<UpdateAtcWindowViewModel>();
            services.AddTransient<CallPageViewModel>();
            services.AddTransient<CreateCallWindowViewModel>();
            services.AddTransient<UpdateCallWindowViewModel>();
            services.AddTransient<AbonentPageViewModel>();
            services.AddTransient<CreateAbonentWindowViewModel>();
            services.AddTransient<UpdateAbonentWindowViewModel>();
            services.AddTransient<BenefitPageViewModel>();
            services.AddTransient<CreateBenefitWindowViewModel>();
            services.AddTransient<UpdateBenefitWindowViewModel>();
            services.AddTransient<CountryPageViewModel>();
            services.AddTransient<CreateCountryWindowViewModel>();
            services.AddTransient<UpdateCountryWindowViewModel>();
            services.AddTransient<CityPageViewModel>();
            services.AddTransient<CreateCityWindowViewModel>();
            services.AddTransient<UpdateCityWindowViewModel>();
            services.AddTransient<AreaPageViewModel>();
            services.AddTransient<CreateAreaWindowViewModel>();
            services.AddTransient<UpdateAreaWindowViewModel>();
            services.AddTransient<TariffPageViewModel>();
            services.AddTransient<CreateTariffWindowViewModel>();
            services.AddTransient<UpdateTariffWindowViewModel>();
            services.AddTransient<SocialStatusPageViewModel>();
            services.AddTransient<CreateSocialStatusWindowViewModel>();
            services.AddTransient<UpdateSocialStatusWindowViewModel>();
            services.AddTransient<BenefitTypePageViewModel>();
            services.AddTransient<CreateBenefitTypeWindowViewModel>();
            services.AddTransient<UpdateBenefitTypeWindowViewModel>();
            services.AddTransient<AbonentsBySocialStatusQueryPageViewModel>();
            services.AddTransient<QueriesControlPageViewModel>();
            services.AddTransient<AreasByCityQueryPageViewModel>();
            services.AddTransient<CallsByCallDateQueryPageViewModel>();
            services.AddTransient<TariffsByEndDateQueryPageViewModel>();
            services.AddTransient<AbonentsInfoQueryPageViewModel>();
            services.AddTransient<AbonentsBenefitInfoQueryPageViewModel>();
            services.AddTransient<AtcesAreaInfoQueryPageViewModel>();
            services.AddTransient<CallsWhereTariffRatioIsNotNullQueryPageViewModel>();
            services.AddTransient<AbonentsHaveCallsQueryPageViewModel>();
            services.AddTransient<AbonentsHaveCallsByPhoneQueryPageViewModel>();
            services.AddTransient<TimeCallsSumQueryPageViewModel>();
            services.AddTransient<CountAbonentsBySocialStatusQueryPageViewModel>();
            services.AddTransient<AbonentsByCallTimeSumQueryPageViewModel>();
            services.AddTransient<SocialStatusByMaskQueryPageViewModel>();
            services.AddTransient<AbonentsByCallsCostSumQueryPageViewModel>();
            services.AddTransient<AbonentsByCallsCostDateSumQueryPageViewModel>();
            services.AddTransient<FirmsSumConnectionCostInflationQueryPageViewModel>();
            services.AddTransient<AreasWithCitiesAndCountriesQueryPageViewModel>();
            services.AddTransient<CallsByAtcAreaNameQueryPageViewModel>();
            services.AddTransient<CallsNoByAtcAreaNameQueryPageViewModel>();
            services.AddTransient<CaseQueryPageViewModel>();
            services.AddTransient<DiffCallCostQueryPageViewModel>();
            services.AddTransient<AtcesPopularStatusesQueryPageViewModel>();
            services.AddTransient<CitiesPopularStatusesQueryPageViewModel>();
            services.AddTransient<AvgCallTimeByAtcQueryPageViewModel>();
            services.AddTransient<AvgCallTimeByCityQueryPageViewModel>();
            services.AddTransient<CitiesCallsQueryPageViewModel>();
            services.AddTransient<CountriesCallsQueryPageViewModel>();

            #endregion

            #region Table Pages

            services.AddTransient<ITablePage, AtcPage>();
            services.AddTransient<ITablePage, CallPage>();
            services.AddTransient<ITablePage, AbonentPage>();
            services.AddTransient<ITablePage, BenefitPage>();
            services.AddTransient<ITablePage, CountryPage>();
            services.AddTransient<ITablePage, CityPage>();
            services.AddTransient<ITablePage, AreaPage>();
            services.AddTransient<ITablePage, TariffPage>();
            services.AddTransient<ITablePage, SocialStatusPage>();
            services.AddTransient<ITablePage, BenefitTypePage>();

            #endregion

            #region Query Pages

            services.AddSingleton<QueriesControlPage>();
            services.AddTransient<IQueryPage, AbonentsBySocialStatusQueryPage>();
            services.AddTransient<IQueryPage, AreasByCityQueryPage>();
            services.AddTransient<IQueryPage, CallsByCallDateQueryPage>();
            services.AddTransient<IQueryPage, TariffsByEndDateQueryPage>();
            services.AddTransient<IQueryPage, AbonentsInfoQueryPage>();
            services.AddTransient<IQueryPage, AbonentsBenefitInfoQueryPage>();
            services.AddTransient<IQueryPage, AtcesAreaInfoQueryPage>();
            services.AddTransient<IQueryPage, CallsWhereTariffRatioIsNotNullQueryPage>();
            services.AddTransient<IQueryPage, AbonentsHaveCallsQueryPage>();
            services.AddTransient<IQueryPage, AbonentsHaveCallsByPhoneQueryPage>();
            services.AddTransient<IQueryPage, TimeCallsSumQueryPage>();
            services.AddTransient<IQueryPage, CountAbonentsBySocialStatusQueryPage>();
            services.AddTransient<IQueryPage, AbonentsByCallTimeSumQueryPage>();
            services.AddTransient<IQueryPage, SocialStatusByMaskQueryPage>();
            services.AddTransient<IQueryPage, AbonentsByCallsCostSumQueryPage>();
            services.AddTransient<IQueryPage, AbonentsByCallsCostDateSumQueryPage>();
            services.AddTransient<IQueryPage, FirmsSumConnectionCostInflationQueryPage>();
            services.AddTransient<IQueryPage, AreasWithCitiesAndCountriesQueryPage>();
            services.AddTransient<IQueryPage, CallsByAtcAreaNameQueryPage>();
            services.AddTransient<IQueryPage, CallsNoByAtcAreaNameQueryPage>();
            services.AddTransient<IQueryPage, CaseQueryPage>();
            services.AddTransient<IQueryPage, DiffCallCostQueryPage>();
            services.AddTransient<IQueryPage, AtcesPopularStatusesQueryPage>();
            services.AddTransient<IQueryPage, CitiesPopularStatusesQueryPage>();
            services.AddTransient<IQueryPage, AvgCallTimeByAtcQueryPage>();
            services.AddTransient<IQueryPage, AvgCallTimeByCityQueryPage>();
            services.AddTransient<IQueryPage, CitiesCallsQueryPage>();
            services.AddTransient<IQueryPage, CountriesCallsQueryPage>();

            #endregion

            #region Queries

            services.AddTransient<IAbonentsBySocialStatusQuery, AbonentsBySocialStatusQuery>();
            services.AddTransient<IAreasByCityQuery, AreasByCityQuery>();
            services.AddTransient<ICallsByCallDateQuery, CallsByCallDateQuery>();
            services.AddTransient<ITariffsByEndDateQuery, TariffsByEndDateQuery>();
            services.AddTransient<IAbonentsInfoQuery, AbonentsInfoQuery>();
            services.AddTransient<IAbonentsBenefitInfoQuery, AbonentsBenefitInfoQuery>();
            services.AddTransient<IAtcesAreaInfoQuery, AtcesAreaInfoQuery>();
            services.AddTransient<ICallsWhereTariffRatioIsNotNullQuery, CallsWhereTariffRatioIsNotNullQuery>();
            services.AddTransient<IAbonentsHaveCallsQuery, AbonentsHaveCallsQuery>();
            services.AddTransient<IAbonentsHaveCallsByPhoneQuery, AbonentsHaveCallsByPhoneQuery>();
            services.AddTransient<ITimeCallsSumQuery, TimeCallsSumQuery>();
            services.AddTransient<ICountAbonentsBySocialStatusQuery, CountAbonentsBySocialStatusQuery>();
            services.AddTransient<IAbonentsByCallTimeSumQuery, AbonentsByCallTimeSumQuery>();
            services.AddTransient<ISocialStatusByMaskQuery, SocialStatusByMaskQuery>();
            services.AddTransient<IAbonentsByCallsCostSumQuery, AbonentsByCallsCostSumQuery>();
            services.AddTransient<IAbonentsByCallsCostDateSumQuery, AbonentsByCallsCostDateSumQuery>();
            services.AddTransient<IFirmsSumConnectionCostInflationQuery, FirmsSumConnectionCostInflationQuery>();
            services.AddTransient<IAreasWithCitiesAndCountriesQuery, AreasWithCitiesAndCountriesQuery>();
            services.AddTransient<ICallsByAtcAreaNameQuery, CallsByAtcAreaNameQuery>();
            services.AddTransient<ICallsNoByAtcAreaNameQuery, CallsNoByAtcAreaNameQuery>();
            services.AddTransient<ICaseQuery, CaseQuery>();
            services.AddTransient<IDiffCallCostQuery, DiffCallCostQuery>();
            services.AddTransient<IAtcesPopularStatusesQuery, AtcesPopularStatusesQuery>();
            services.AddTransient<ICitiesPopularStatusesQuery, CitiesPopularStatusesQuery>();
            services.AddTransient<IAvgCallTimeByAtcQuery, AvgCallTimeByAtcQuery>();
            services.AddTransient<IAvgCallTimeByCityQuery, AvgCallTimeByCityQuery>();
            services.AddTransient<ICitiesCallsQuery, CitiesCallsQuery>();
            services.AddTransient<ICountriesCallsQuery, CountriesCallsQuery>();

            #endregion

            services.AddSingleton(new MessageBus());
            services.AddSingleton(new EventBus());

            _serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
    }
}

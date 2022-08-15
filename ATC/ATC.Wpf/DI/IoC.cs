using ATC.Wpf.Abstractions;
using ATC.Wpf.Repositories;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Services.DataGenerators;
using ATC.Wpf.ViewModels;
using ATC.Wpf.ViewModels.Tables.Abonent;
using ATC.Wpf.ViewModels.Tables.Atc;
using ATC.Wpf.ViewModels.Tables.Benefit;
using ATC.Wpf.ViewModels.Tables.Call;
using ATC.Wpf.ViewModels.Tables.Country;
using ATC.Wpf.Views.Tables;
using ATC.Wpf.Views.Tables.Abonent;
using ATC.Wpf.Views.Tables.Benefit;
using ATC.Wpf.Views.Tables.Call;
using ATC.Wpf.Views.Tables.Country;
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

            #endregion

            #region Table Pages

            services.AddTransient<ITablePage, AtcPage>();
            services.AddTransient<ITablePage, CallPage>();
            services.AddTransient<ITablePage, AbonentPage>();
            services.AddTransient<ITablePage, BenefitPage>();
            services.AddTransient<ITablePage, CountryPage>();

            #endregion

            services.AddSingleton(new MessageBus());
            services.AddSingleton(new EventBus());

            _serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
    }
}

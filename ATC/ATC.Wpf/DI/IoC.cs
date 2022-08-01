using ATC.Wpf.Abstractions;
using ATC.Wpf.Repositories;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using ATC.Wpf.Services.DataGenerators;
using ATC.Wpf.ViewModels;
using ATC.Wpf.Views.Tables;
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

            services.AddSingleton(new NpgsqlConnection("server=127.0.0.1;port=5432;database=ATCDB;user id=postgres;password=9156;Pooling=true;Timeout=60;Command Timeout=60;"));

            #endregion

            #region Repositories

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<ITariffRepository, TariffRepository>();
            services.AddTransient<IBenefitTypeRepository, BenefitTypeRepository>();
            services.AddTransient<ISocialStatusRepository, SocialStatusRepository>();

            #endregion

            #region Data Generators

            services.AddTransient<CityGenerator>();
            services.AddTransient<CountryGenerator>();
            services.AddTransient<AreaGenerator>();
            services.AddTransient<TariffGenerator>();
            services.AddTransient<BenefitTypeGenerator>();
            services.AddTransient<SocialStatusGenerator>();
            services.AddTransient<DataInitializer>();

            #endregion

            #region ViewModels

            services.AddTransient<SignInWindowViewModel>();
            services.AddTransient<MainWindowViewModel>();

            #endregion

            #region Table Pages

            services.AddTransient<ITablePage, AtcPage>();

            #endregion

            _serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Country
{
    internal class CreateCountryWindowViewModel : BindableBase
    {
        private readonly ICountryRepository _repository;
        private readonly EventBus _eventBus;

        public CreateCountryWindowViewModel(ICountryRepository repository,
            EventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;

            Country = new();
        }

        public CountryModel Country { get; set; }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _repository.Create(Country);
                await _eventBus.Publish(new CountryNewModelEvent { Country = Country });
                window.Close();
                MessageBoxManager.ShowInformation("Страна успешно добавлена.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _repository.DisposeAsync();
            }
        });
    }
}

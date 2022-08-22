using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Country
{
    internal class UpdateCountryWindowViewModel : BindableBase
    {
        private readonly ICountryRepository _repository;
        private readonly EventBus _eventBus;

        public UpdateCountryWindowViewModel(ICountryRepository repository,
            EventBus eventBus, MessageBus messageBus)
        {
            _repository = repository;
            _eventBus = eventBus;

            Country = new();

            messageBus.Recieve<AbstractModelMessage<CountryModel>>(this, (msg) =>
            {
                Country = msg.Model;
                return Task.CompletedTask;
            });
        }

        public CountryModel Country { get; set; }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _repository.Update(Country);
                await _eventBus.Publish(new AbstractUpdateModelEvent<CountryModel> { Model = Country });
                window.Close();
                MessageBoxManager.ShowInformation("Страна успешно обновлена.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _repository.DisposeAsync();
            }
        });
    }
}

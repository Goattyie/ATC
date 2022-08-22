using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Tariff
{
    internal class UpdateTariffWindowViewModel : BindableBase
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly EventBus _eventBus;

        public UpdateTariffWindowViewModel(ITariffRepository tariffRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _tariffRepository = tariffRepository;
            _eventBus = eventBus;

            Tariff = new();

            messageBus.Recieve<AbstractModelMessage<TariffModel>>(this, (msg) =>
            {
                Tariff = msg.Model;
                return Task.CompletedTask;
            });
        }

        public TariffModel Tariff { get; set; }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _tariffRepository.Update(Tariff);
                await _eventBus.Publish(new AbstractUpdateModelEvent<TariffModel>{ Model = Tariff });
                window.Close();
                MessageBoxManager.ShowInformation("Тариф успешно обновлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _tariffRepository.DisposeAsync();
            }
        });
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.Tariff
{
    internal class CreateTariffWindowViewModel : BindableBase
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly EventBus _eventBus;

        public CreateTariffWindowViewModel(ITariffRepository tariffRepository,
            EventBus eventBus)
        {
            _tariffRepository = tariffRepository;
            _eventBus = eventBus;

            Tariff = new() { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) };
        }

        public TariffModel Tariff { get; set; }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _tariffRepository.Create(Tariff);
                await _eventBus.Publish(new AbstractNewModelEvent<TariffModel> { Model = Tariff });
                window.Close();
                MessageBoxManager.ShowInformation("Тариф успешно добавлен.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _tariffRepository.DisposeAsync();
            }
        });
    }
}

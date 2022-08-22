using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;
using System.Windows;

namespace ATC.Wpf.ViewModels.Tables.SocialStatus
{
    internal class CreateSocialStatusWindowViewModel : BindableBase
    {
        private readonly ISocialStatusRepository _repository;
        private readonly EventBus _eventBus;

        public CreateSocialStatusWindowViewModel(ISocialStatusRepository repository,
            EventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;

            SocialStatus = new();
        }

        public SocialStatusModel SocialStatus { get; set; }

        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _repository.Create(SocialStatus);
                await _eventBus.Publish(new AbstractNewModelEvent<SocialStatusModel> { Model = SocialStatus });
                window.Close();
                MessageBoxManager.ShowInformation("Социальное положение успешно добавлено.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _repository.DisposeAsync();
            }
        });
    }
}

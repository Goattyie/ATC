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

namespace ATC.Wpf.ViewModels.Tables.SocialStatus
{
    internal class UpdateSocialStatusWindowViewModel : BindableBase
    {
        private readonly ISocialStatusRepository _repository;
        private readonly EventBus _eventBus;

        public UpdateSocialStatusWindowViewModel(ISocialStatusRepository repository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _repository = repository;
            _eventBus = eventBus;

            SocialStatus = new();

            messageBus.Recieve<AbstractModelMessage<SocialStatusModel>>(this, (msg) =>
            {
                SocialStatus = msg.Model;
                return Task.CompletedTask;
            });
        }

        public SocialStatusModel SocialStatus { get; set; }

        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            try
            {
                await _repository.Update(SocialStatus);
                await _eventBus.Publish(new AbstractUpdateModelEvent<SocialStatusModel> { Model = SocialStatus });
                window.Close();
                MessageBoxManager.ShowInformation("Социальное положение успешно обновлено.");

            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _repository.DisposeAsync();
            }
        });
    }
}
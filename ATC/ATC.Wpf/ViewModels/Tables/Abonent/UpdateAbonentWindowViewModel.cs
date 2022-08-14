using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ATC.Wpf.ViewModels.Tables.Abonent
{
    internal class UpdateAbonentWindowViewModel : BindableBase
    {
        private readonly IAbonentRepository _abonentRepository;
        private readonly IBenefitRepository _benefitRepository;
        private readonly ISocialStatusRepository _socialStatusRepository;
        private readonly EventBus _eventBus;

        public UpdateAbonentWindowViewModel(IAbonentRepository abonentRepository,
            IBenefitRepository benefitRepository,
            ISocialStatusRepository socialStatusRepository,
            EventBus eventBus,
            MessageBus messageBus)
        {
            _abonentRepository = abonentRepository;
            _benefitRepository = benefitRepository;
            _socialStatusRepository = socialStatusRepository;
            _eventBus = eventBus;

            Abonent = new();
            Benefits = new();
            SocialStatuses = new();

            messageBus.Recieve<AbonentModelMessage>(this, LoadData);
        }

        public ImageSource ImageSource { get; set; }
        public AbonentModel Abonent { get; set; }
        public ObservableCollection<Benefit> Benefits { get; set; }
        public Benefit SelectedBenefit { get; set; }
        public ObservableCollection<SocialStatus> SocialStatuses { get; set; }
        public SocialStatus SelectedSocialStatus { get; set; }
        public IDelegateCommand LoadPhoto => new DelegateCommand(() =>
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".jpg";
            fileDialog.Filter = "(.jpg)|*.jpg";

            var dialogResult = fileDialog.ShowDialog();

            if (!dialogResult.HasValue || !dialogResult.Value)
                return;

            var newFilePath = $"{AppConfiguration.PhotosFolder + Guid.NewGuid()}.jpg";

            File.Copy(fileDialog.FileName, newFilePath);

            Abonent.Photo = newFilePath;
            LoadImage(newFilePath);
        });
        public IAsyncCommand UpdateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Abonent.BenefitId = SelectedBenefit?.Id ?? 0;
            Abonent.SocialStatusId = SelectedSocialStatus?.Id ?? 0;
            Abonent.SocialStatusName = SelectedSocialStatus?.Name;

            try
            {
                await _abonentRepository.Update(Abonent);
                await _eventBus.Publish(new AbonentUpdateModelEvent { Abonent = Abonent });
                window.Close();
                MessageBoxManager.ShowInformation("Абонент успешно обновлен.");
            }
            catch (Exception ex)
            {
                MessageBoxManager.ShowError(ex.Message);
                await _abonentRepository.DisposeAsync();
            }
        });
        private void LoadImage(string path)
        {
            Stream reader = File.OpenRead(path);
            System.Drawing.Image photo = System.Drawing.Image.FromStream(reader);

            MemoryStream finalStream = new MemoryStream();
            photo.Save(finalStream, ImageFormat.Png);


            var decoder = new PngBitmapDecoder(finalStream, BitmapCreateOptions.PreservePixelFormat,
                                         BitmapCacheOption.Default);
            ImageSource = decoder.Frames[0];
        }

        private async Task LoadData(AbonentModelMessage msg)
        {
            Abonent = msg.Abonent;

            Benefits.Clear();
            SocialStatuses.Clear();

            var benefits = await _benefitRepository.Get();
            var statuses = await _socialStatusRepository.Get();

            foreach (var item in benefits)
                Benefits.Add(item);

            foreach (var item in statuses)
                SocialStatuses.Add(item);

            SelectedBenefit = Benefits.FirstOrDefault(x => x.Id == Abonent.BenefitId);
            SelectedSocialStatus = SocialStatuses.FirstOrDefault(x => x.Id == Abonent.SocialStatusId);

            LoadImage(Abonent.Photo);
        }
    }
}

using ATC.Wpf.Models;
using ATC.Wpf.Repositories.Interfaces;
using ATC.Wpf.Resources;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace ATC.Wpf.ViewModels.Tables.Abonent
{
    internal class CreateAbonentWindowViewModel : BindableBase
    {
        private readonly IAbonentRepository _abonentRepository;
        private readonly IBenefitRepository _benefitRepository;
        private readonly ISocialStatusRepository _socialStatusRepository;
        private readonly EventBus _eventBus;

        public CreateAbonentWindowViewModel(IAbonentRepository abonentRepository,
            IBenefitRepository benefitRepository,
            ISocialStatusRepository socialStatusRepository,
            EventBus eventBus)
        {
            _abonentRepository = abonentRepository;
            _benefitRepository = benefitRepository;
            _socialStatusRepository = socialStatusRepository;
            _eventBus = eventBus;

            Abonent = new();
            Benefits = new();
            SocialStatuses = new();

            LoadData();
        }

        public ImageSource ImageSource { get; set; }
        public AbonentModel Abonent { get; set; }
        public ObservableCollection<BenefitModel> Benefits { get; set; }
        public BenefitModel SelectedBenefit { get; set; }
        public ObservableCollection<SocialStatusModel> SocialStatuses { get; set; }
        public SocialStatusModel SelectedSocialStatus { get; set; }
        public IDelegateCommand LoadPhoto => new DelegateCommand(() =>
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".jpg";
            fileDialog.Filter = "(.jpg)|*.jpg";

            var dialogResult = fileDialog.ShowDialog();

            if(!dialogResult.HasValue || !dialogResult.Value)
                return;

            var newFilePath = $"{AppConfiguration.PhotosFolder + Guid.NewGuid()}.jpg";

            File.Copy(fileDialog.FileName, newFilePath);

            Abonent.Photo = newFilePath;
            LoadImage(newFilePath);
        });
        public IAsyncCommand CreateCommand => new AsyncCommand<Window>(async (Window window) =>
        {
            Abonent.BenefitId = SelectedBenefit?.Id ?? 0;
            Abonent.SocialStatusId = SelectedSocialStatus?.Id ?? 0;
            Abonent.SocialStatusName = SelectedSocialStatus?.Name;

            try
            {
                await _abonentRepository.Create(Abonent);
                await _eventBus.Publish(new AbstractNewModelEvent<AbonentModel> { Model = Abonent });
                window.Close();
                MessageBoxManager.ShowInformation("Абонент успешно добавлен.");
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

        private async Task LoadData()
        {
            Benefits.Clear();
            SocialStatuses.Clear();

            var benefits = await _benefitRepository.Get();
            var statuses = await _socialStatusRepository.Get();

            foreach (var item in benefits)
                Benefits.Add(item);

            foreach (var item in statuses)
                SocialStatuses.Add(item);

            SelectedBenefit = Benefits.FirstOrDefault();
            SelectedSocialStatus = SocialStatuses.FirstOrDefault();
        }
    }
}

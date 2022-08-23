using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;
using ATC.Wpf.ViewModels.Queries;
using ATC.Wpf.ViewModels.Tables.Abonent;
using ATC.Wpf.ViewModels.Tables.Area;
using ATC.Wpf.ViewModels.Tables.Atc;
using ATC.Wpf.ViewModels.Tables.Benefit;
using ATC.Wpf.ViewModels.Tables.BenefitType;
using ATC.Wpf.ViewModels.Tables.Call;
using ATC.Wpf.ViewModels.Tables.City;
using ATC.Wpf.ViewModels.Tables.Country;
using ATC.Wpf.ViewModels.Tables.SocialStatus;
using ATC.Wpf.ViewModels.Tables.Tariff;

namespace ATC.Wpf.Services
{
    internal class ViewModelLocator
    {
        public SignInWindowViewModel SignInWindowView => IoC.Resolve<SignInWindowViewModel>();
        public MainWindowViewModel MainWindowViewModel => IoC.Resolve<MainWindowViewModel>();
        public AtcPageViewModel AtcPageViewModel => IoC.Resolve<AtcPageViewModel>();
        public CreateAtcWindowViewModel CreateAtcWindowViewModel => IoC.Resolve<CreateAtcWindowViewModel>();
        public UpdateAtcWindowViewModel UpdateAtcWindowViewModel => IoC.Resolve<UpdateAtcWindowViewModel>();
        public CallPageViewModel CallPageViewModel => IoC.Resolve<CallPageViewModel>();
        public CreateCallWindowViewModel CreateCallWindowViewModel => IoC.Resolve<CreateCallWindowViewModel>();
        public UpdateCallWindowViewModel UpdateCallWindowViewModel => IoC.Resolve<UpdateCallWindowViewModel>();
        public AbonentPageViewModel AbonentPageViewModel => IoC.Resolve<AbonentPageViewModel>();
        public CreateAbonentWindowViewModel CreateAbonentWindowViewModel => IoC.Resolve<CreateAbonentWindowViewModel>();
        public UpdateAbonentWindowViewModel UpdateAbonentWindowViewModel => IoC.Resolve<UpdateAbonentWindowViewModel>();
        public BenefitPageViewModel BenefitPageViewModel => IoC.Resolve<BenefitPageViewModel>();
        public CreateBenefitWindowViewModel CreateBenefitWindowViewModel => IoC.Resolve<CreateBenefitWindowViewModel>();
        public UpdateBenefitWindowViewModel UpdateBenefitWindowViewModel => IoC.Resolve<UpdateBenefitWindowViewModel>();
        public CountryPageViewModel CountryPageViewModel => IoC.Resolve<CountryPageViewModel>();
        public CreateCountryWindowViewModel CreateCountryWindowViewModel => IoC.Resolve<CreateCountryWindowViewModel>();
        public UpdateCountryWindowViewModel UpdateCountryWindowViewModel => IoC.Resolve<UpdateCountryWindowViewModel>();
        public CityPageViewModel CityPageViewModel => IoC.Resolve<CityPageViewModel>();
        public CreateCityWindowViewModel CreateCityWindowViewModel => IoC.Resolve<CreateCityWindowViewModel>();
        public UpdateCityWindowViewModel UpdateCityWindowViewModel => IoC.Resolve<UpdateCityWindowViewModel>();
        public AreaPageViewModel AreaPageViewModel => IoC.Resolve<AreaPageViewModel>();
        public CreateAreaWindowViewModel CreateAreaWindowViewModel => IoC.Resolve<CreateAreaWindowViewModel>();
        public UpdateAreaWindowViewModel UpdateAreaWindowViewModel => IoC.Resolve<UpdateAreaWindowViewModel>();
        public TariffPageViewModel TariffPageViewModel => IoC.Resolve<TariffPageViewModel>();
        public CreateTariffWindowViewModel CreateTariffWindowViewModel => IoC.Resolve<CreateTariffWindowViewModel>();
        public UpdateTariffWindowViewModel UpdateTariffWindowViewModel => IoC.Resolve<UpdateTariffWindowViewModel>();
        public SocialStatusPageViewModel SocialStatusPageViewModel => IoC.Resolve<SocialStatusPageViewModel>();
        public CreateSocialStatusWindowViewModel CreateSocialStatusWindowViewModel => IoC.Resolve<CreateSocialStatusWindowViewModel>();
        public UpdateSocialStatusWindowViewModel UpdateSocialStatusWindowViewModel => IoC.Resolve<UpdateSocialStatusWindowViewModel>();
        public BenefitTypePageViewModel BenefitTypePageViewModel => IoC.Resolve<BenefitTypePageViewModel>();
        public CreateBenefitTypeWindowViewModel CreateBenefitTypeWindowViewModel => IoC.Resolve<CreateBenefitTypeWindowViewModel>();
        public UpdateBenefitTypeWindowViewModel UpdateBenefitTypeWindowViewModel => IoC.Resolve<UpdateBenefitTypeWindowViewModel>();
        public AbonentsBySocialStatusQueryPageViewModel AbonentsBySocialStatusQueryPageViewModel => IoC.Resolve<AbonentsBySocialStatusQueryPageViewModel>();
        public QueriesControlPageViewModel QueriesControlPageViewModel => IoC.Resolve<QueriesControlPageViewModel>();
        public AreasByCityQueryPageViewModel AreasByCityQueryPageViewModel => IoC.Resolve<AreasByCityQueryPageViewModel>();
        public CallsByCallDateQueryPageViewModel CallsByCallDateQueryViewModel => IoC.Resolve<CallsByCallDateQueryPageViewModel>();
        public TariffsByEndDateQueryPageViewModel TariffsByEndDateQueryPageViewModel => IoC.Resolve<TariffsByEndDateQueryPageViewModel>();
        public AbonentsInfoQueryPageViewModel AbonentsInfoQueryPageViewModel => IoC.Resolve<AbonentsInfoQueryPageViewModel>();
    }
}
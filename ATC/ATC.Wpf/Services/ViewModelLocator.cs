using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;
using ATC.Wpf.ViewModels.Tables.Abonent;
using ATC.Wpf.ViewModels.Tables.Atc;
using ATC.Wpf.ViewModels.Tables.Benefit;
using ATC.Wpf.ViewModels.Tables.Call;

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
    }
}
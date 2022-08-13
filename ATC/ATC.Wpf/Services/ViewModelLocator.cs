using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;
using ATC.Wpf.ViewModels.Tables.Atc;

namespace ATC.Wpf.Services
{
    internal class ViewModelLocator
    {
        public SignInWindowViewModel SignInWindowView => IoC.Resolve<SignInWindowViewModel>();
        public MainWindowViewModel MainWindowViewModel => IoC.Resolve<MainWindowViewModel>();
        public AtcPageViewModel AtcPageViewModel => IoC.Resolve<AtcPageViewModel>();
        public CreateAtcWindowViewModel CreateAtcWindowViewModel => IoC.Resolve<CreateAtcWindowViewModel>();
        public UpdateAtcWindowViewModel UpdateAtcWindowViewModel => IoC.Resolve<UpdateAtcWindowViewModel>();
    }
}

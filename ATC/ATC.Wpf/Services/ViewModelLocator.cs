using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;

namespace ATC.Wpf.Services
{
    internal class ViewModelLocator
    {
        public SignInWindowViewModel SignInWindowView => IoC.Resolve<SignInWindowViewModel>();
        public MainWindowViewModel MainWindowViewModel => IoC.Resolve<MainWindowViewModel>();
    }
}

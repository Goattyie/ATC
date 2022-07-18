using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;
using System;

namespace ATC.Wpf.Services
{
    internal class ViewModelLocator
    {
        public SignInWindowViewModel SignInWindowView => IoC.Resolve<SignInWindowViewModel>();
    }
}

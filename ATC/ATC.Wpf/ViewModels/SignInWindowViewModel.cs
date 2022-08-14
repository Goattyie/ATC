using ATC.Wpf.Resources;
using ATC.Wpf.Services;
using DevExpress.Mvvm;
using System;

namespace ATC.Wpf.ViewModels
{
    internal class SignInWindowViewModel : BindableBase
    {
        private const string LOGIN = "root";
        private const string PASSWORD = "1234";

        public SignInWindowViewModel()
        {
            Login = LOGIN;
            Password = PASSWORD;
        }
        
        public Action OnClose { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IDelegateCommand SignIn => new DelegateCommand(() =>
        {
            if (Login != LOGIN)
            {
                MessageBoxManager.ShowError(Res.WrongLogin);
                return;
            }

            if(Password != PASSWORD)
            {
                MessageBoxManager.ShowError(Res.WrongPassword);
                return;
            }

            OnClose?.Invoke();
        });
    }
}

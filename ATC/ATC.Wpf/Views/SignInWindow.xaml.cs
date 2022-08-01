using ATC.Wpf.DI;
using ATC.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ATC.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

            var vm = IoC.Resolve<SignInWindowViewModel>();

            vm.OnClose += () =>
            {
                new MainWindow().Show();
                Close();
            };

            DataContext = vm;
        }
    }
}

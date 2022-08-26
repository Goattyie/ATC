using ATC.Wpf.Abstractions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATC.Wpf.Views.Queries
{
    /// <summary>
    /// Interaction logic for SocialStatusByMaskQueryPage.xaml
    /// </summary>
    public partial class SocialStatusByMaskQueryPage : Page, IQueryPage
    {
        public SocialStatusByMaskQueryPage()
        {
            InitializeComponent();
        }

        public string QueryTitle => "Вывести социальные положения и их количество абонентов, в которых встречается введенная подстрока";
    }
}

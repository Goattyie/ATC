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
    /// Interaction logic for CitiesPopularStatusesQueryPage.xaml
    /// </summary>
    public partial class CitiesPopularStatusesQueryPage : Page, IQueryPage
    {
        public CitiesPopularStatusesQueryPage()
        {
            InitializeComponent();
        }

        public string QueryTitle => "Кто чаще из соц. положений пользуется услугами АТС (города)";
    }
}

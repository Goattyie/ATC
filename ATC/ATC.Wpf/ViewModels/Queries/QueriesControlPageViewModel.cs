using ATC.Wpf.Abstractions;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ATC.Wpf.ViewModels.Queries
{
    internal class QueriesControlPageViewModel : BindableBase
    {
        private readonly IEnumerable<IQueryPage> _pages;
        private string _selectedTitle;

        public QueriesControlPageViewModel(IEnumerable<IQueryPage> pages)
        {
            _pages = pages;

            Titles = _pages.Select(x => x.QueryTitle);
            SelectedTitle = Titles.FirstOrDefault();
            CurrentPage = (Page)_pages.FirstOrDefault();
        }

        public Page CurrentPage { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public string SelectedTitle 
        { 
            get => _selectedTitle;
            set
            {
                _selectedTitle = value;

                CurrentPage = (Page)_pages.FirstOrDefault(x => x.QueryTitle == value);
            } 
        }
    }
}

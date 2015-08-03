using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DongY
{
    public partial class Benh : PhoneApplicationPage
    {
        List<Catalog> _listCatalog;

        public Benh()
        {
            InitializeComponent();
            loadData();
            llistCatalog.SelectionChanged += new SelectionChangedEventHandler(slc_click);
        }

        private void loadData()
        {
            string strSelect = "SELECT * FROM Catalog ORDER BY Idc ASC";
            _listCatalog = (Application.Current as App).dp.SelectList<Catalog>(strSelect);
            llistCatalog.ItemsSource = _listCatalog;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void slc_click(object sender, SelectionChangedEventArgs e)
        {
            Catalog tg = e.AddedItems[0] as Catalog;
            NavigationService.Navigate(new Uri(String.Format("/Benh-Items.xaml?idc={0}&name={1}", tg.Idc, tg.Note), UriKind.Relative));
        }

    }
}
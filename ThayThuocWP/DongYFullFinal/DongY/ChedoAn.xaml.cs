using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Net.NetworkInformation;

namespace DongY
{
    public partial class ChedoAn : PhoneApplicationPage
    {
        List<DietItem> _listDietItem;
        public ChedoAn()
        {
            InitializeComponent();
            loadData();
            llistDiet.SelectionChanged += new SelectionChangedEventHandler(slc_click);
        }

        private void loadData()
        {
            string strSelect = "SELECT * FROM CheDoAn ORDER BY id ASC";
            _listDietItem = (Application.Current as App).dp.SelectList<DietItem>(strSelect);
            llistDiet.ItemsSource = _listDietItem;
        }

        private void slc_click(object sender, SelectionChangedEventArgs e)
        {
            bool isInternetOn = NetworkInterface.GetIsNetworkAvailable();
            if (!isInternetOn)
            {
                MessageBox.Show("Your are not connecting to internet");
            }
            else
            {
                DietItem tg = e.AddedItems[0] as DietItem;
                NavigationService.Navigate(new Uri(String.Format("/ChedoAn-Detail.xaml?navi={0}", tg.URL), UriKind.Relative));
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
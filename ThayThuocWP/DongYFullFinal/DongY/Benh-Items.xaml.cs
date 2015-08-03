using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.NetworkInformation;

namespace DongY
{
    public partial class Benh_Items : PhoneApplicationPage
    {

        List<Item> _listItem; 
        public Benh_Items()
        {
            InitializeComponent();
            llistItem.SelectionChanged += new SelectionChangedEventHandler(slc_click);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string idc = NavigationContext.QueryString["idc"];
            Title_Parent.Text = NavigationContext.QueryString["name"];
            loadData(idc);
        }

        private void loadData(string idc)
        {
            string strSelect = "SELECT * from Items where Idc=" + idc;
            _listItem = (Application.Current as App).dp.SelectList<Item>(strSelect);
            llistItem.ItemsSource = _listItem;
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
                Item tg = e.AddedItems[0] as Item;
                NavigationService.Navigate(new Uri(String.Format("/Benh-Item-Detail.xaml?navi={0}", tg.Link), UriKind.Relative));
            }

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
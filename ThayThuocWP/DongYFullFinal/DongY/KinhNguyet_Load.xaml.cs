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
    public partial class KinhNguyet_Load : PhoneApplicationPage
    {

        List<Cycle> _listCycle;

        public KinhNguyet_Load()
        {
            InitializeComponent();
            loadData();
            llistItem.SelectionChanged += new SelectionChangedEventHandler(slc_click);
        
        }
        private void loadData()
        {
            string strSelect = "SELECT * FROM Cycle ORDER BY id DESC";
            _listCycle = (Application.Current as App).dp.SelectList<Cycle>(strSelect);
            if (_listCycle == null)
            {
                MessageBox.Show("Bạn chưa từng lưu biểu đồ nào trước đây!");
                button_back_Click(new object(), new RoutedEventArgs() );
            }
            llistItem.ItemsSource = _listCycle;

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(String.Format("/KinhNguyet.xaml?id={0}", -1), UriKind.Relative));
        }

        private void slc_click(object sender, SelectionChangedEventArgs e)
        {
            Cycle tg = e.AddedItems[0] as Cycle;
            NavigationService.Navigate(new Uri(String.Format("/KinhNguyet.xaml?id={0}", tg.id), UriKind.Relative));
        }
    }
}
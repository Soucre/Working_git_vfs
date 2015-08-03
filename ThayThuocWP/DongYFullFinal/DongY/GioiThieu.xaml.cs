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

namespace DongY
{
    public partial class GioiThieu : PhoneApplicationPage
    {
        public GioiThieu()
        {
            InitializeComponent();

        }

        private void button_tuVan_Click_1(object sender, RoutedEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = "19006834";


            phoneCallTask.Show();
        }
    }
}
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
    public partial class ChedoAn_Detail : PhoneApplicationPage
    {
        public ChedoAn_Detail()
        {
            InitializeComponent();
        }

        bool Navi = false;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string idc = NavigationContext.QueryString["navi"];

            webbrowser1.Navigated += webbrowser1_Navigated;
            webbrowser1.Navigating += webbrowser1_Navigating;
            webbrowser1.Loaded += (object sender, RoutedEventArgs ex) =>
            {
                webbrowser1.Navigate(new Uri(idc, UriKind.Absolute));
            };

        }

        void webbrowser1_Navigating(object sender, NavigatingEventArgs e)
        {
            if (Navi)
                e.Cancel = true;
        }

        //string script =
        //    "var links = document.getElementsByTagName('a');" +
        //    "for (var index = 0; index < links.length; ++index) { " +
        //    "       links[index].setAttribute('href','#');" +
        //    "}";
        void webbrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            //    WebBrowser wb = (WebBrowser)sender;
            //    wb.InvokeScript("eval", this.script);
            Navi = true;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
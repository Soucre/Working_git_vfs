using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DongY.Resources;

namespace DongY
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}


        // Event: Phone's Orientation Changed
        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            // Switch the placement of the buttons based on an orientation change.
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {
                //Button "Tư Vấn": (0,0)
                Grid.SetRow(button_tuVan, 0);
                Grid.SetColumn(button_tuVan, 0);

                //Text "Tư Vấn": (1,0)
                Grid.SetRow(textblock_tuVan, 1);
                Grid.SetColumn(textblock_tuVan, 0);

                //Button "Giới Thiệu": (0,1)
                Grid.SetRow(button_gioiThieu, 0);
                Grid.SetColumn(button_gioiThieu, 1);

                //Text "Giới Thiệu": (1,1)
                Grid.SetRow(textblock_gioiThieu, 1);
                Grid.SetColumn(textblock_gioiThieu, 1);

            }

            // If not in the portrait mode, move buttonList content to visible row and column.
            else
            {
                //Button "Tư Vấn": (2,2)
                Grid.SetRow(button_tuVan, 2);
                Grid.SetColumn(button_tuVan, 2);

                //Text "Tư Vấn": (3,2)
                Grid.SetRow(textblock_tuVan, 3);
                Grid.SetColumn(textblock_tuVan, 2);

                //Button "Giới Thiệu": (4,1)
                Grid.SetRow(button_gioiThieu, 4);
                Grid.SetColumn(button_gioiThieu, 2);

                //Text "Giới Thiệu": (5,1)
                Grid.SetRow(textblock_gioiThieu, 5);
                Grid.SetColumn(textblock_gioiThieu, 2);

                
            }
        }

    }
}
﻿#pragma checksum "F:\Download\DongYFullFinal\DongY\Benh.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB0381E8383DAAFAD8883DC99D24E58B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace DongY {
    
    
    public partial class Benh : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Canvas title_canvas;
        
        internal System.Windows.Controls.Button button_back;
        
        internal System.Windows.Controls.TextBlock textblock_title;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.LongListSelector llistCatalog;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/DongY;component/Benh.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.title_canvas = ((System.Windows.Controls.Canvas)(this.FindName("title_canvas")));
            this.button_back = ((System.Windows.Controls.Button)(this.FindName("button_back")));
            this.textblock_title = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_title")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.llistCatalog = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llistCatalog")));
        }
    }
}


﻿#pragma checksum "F:\Download\DongYFullFinal\DongY\KinhNguyet.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3EB072696C9193F8746D5BFA4A5A1C44"
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
    
    
    public partial class KinhNguyet : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.DataTemplate lpkItemTemplate;
        
        internal System.Windows.DataTemplate lpkFullItemTemplate;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Canvas title_canvas;
        
        internal System.Windows.Controls.Button button_back;
        
        internal System.Windows.Controls.TextBlock textblock_title;
        
        internal System.Windows.Controls.ScrollViewer content_scrollviewer;
        
        internal System.Windows.Controls.Grid grid_label_1;
        
        internal System.Windows.Controls.TextBlock textblock_canNang;
        
        internal System.Windows.Controls.Grid grid_ngayDau;
        
        internal Microsoft.Phone.Controls.DatePicker datepicker_ngayDau;
        
        internal System.Windows.Controls.Grid grid_chuKyKinh;
        
        internal System.Windows.Controls.TextBlock textblock_chuKiKinh;
        
        internal Microsoft.Phone.Controls.ListPicker listpicker_chuKiKinh;
        
        internal System.Windows.Controls.Grid grid_soNgayCoKinh;
        
        internal System.Windows.Controls.TextBlock textblock_soNgayCoKinh;
        
        internal Microsoft.Phone.Controls.ListPicker listpicker_soNgayCoKinh;
        
        internal System.Windows.Controls.Grid grid_noiDung;
        
        internal Microsoft.Phone.Controls.WebBrowser wbChart;
        
        internal System.Windows.Controls.Grid grid_chuThich;
        
        internal System.Windows.Controls.Grid grid_Save_Load;
        
        internal System.Windows.Controls.Button button_xemLich;
        
        internal System.Windows.Controls.Button button_save;
        
        internal System.Windows.Controls.Button button_load;
        
        internal System.Windows.Controls.Grid grid_coKinh;
        
        internal System.Windows.Controls.TextBlock textblock_coKinh;
        
        internal System.Windows.Controls.Grid grid_quanHeTuDo;
        
        internal System.Windows.Controls.TextBlock textblock_quanHeTuDo;
        
        internal System.Windows.Controls.Grid grid_coTheCoThai;
        
        internal System.Windows.Controls.TextBlock textblock_coTheCoThai;
        
        internal System.Windows.Controls.Grid grid_RungTrung;
        
        internal System.Windows.Controls.TextBlock textblock_RungTrung;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DongY;component/KinhNguyet.xaml", System.UriKind.Relative));
            this.lpkItemTemplate = ((System.Windows.DataTemplate)(this.FindName("lpkItemTemplate")));
            this.lpkFullItemTemplate = ((System.Windows.DataTemplate)(this.FindName("lpkFullItemTemplate")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.title_canvas = ((System.Windows.Controls.Canvas)(this.FindName("title_canvas")));
            this.button_back = ((System.Windows.Controls.Button)(this.FindName("button_back")));
            this.textblock_title = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_title")));
            this.content_scrollviewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("content_scrollviewer")));
            this.grid_label_1 = ((System.Windows.Controls.Grid)(this.FindName("grid_label_1")));
            this.textblock_canNang = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_canNang")));
            this.grid_ngayDau = ((System.Windows.Controls.Grid)(this.FindName("grid_ngayDau")));
            this.datepicker_ngayDau = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("datepicker_ngayDau")));
            this.grid_chuKyKinh = ((System.Windows.Controls.Grid)(this.FindName("grid_chuKyKinh")));
            this.textblock_chuKiKinh = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_chuKiKinh")));
            this.listpicker_chuKiKinh = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listpicker_chuKiKinh")));
            this.grid_soNgayCoKinh = ((System.Windows.Controls.Grid)(this.FindName("grid_soNgayCoKinh")));
            this.textblock_soNgayCoKinh = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_soNgayCoKinh")));
            this.listpicker_soNgayCoKinh = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listpicker_soNgayCoKinh")));
            this.grid_noiDung = ((System.Windows.Controls.Grid)(this.FindName("grid_noiDung")));
            this.wbChart = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("wbChart")));
            this.grid_chuThich = ((System.Windows.Controls.Grid)(this.FindName("grid_chuThich")));
            this.grid_Save_Load = ((System.Windows.Controls.Grid)(this.FindName("grid_Save_Load")));
            this.button_xemLich = ((System.Windows.Controls.Button)(this.FindName("button_xemLich")));
            this.button_save = ((System.Windows.Controls.Button)(this.FindName("button_save")));
            this.button_load = ((System.Windows.Controls.Button)(this.FindName("button_load")));
            this.grid_coKinh = ((System.Windows.Controls.Grid)(this.FindName("grid_coKinh")));
            this.textblock_coKinh = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_coKinh")));
            this.grid_quanHeTuDo = ((System.Windows.Controls.Grid)(this.FindName("grid_quanHeTuDo")));
            this.textblock_quanHeTuDo = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_quanHeTuDo")));
            this.grid_coTheCoThai = ((System.Windows.Controls.Grid)(this.FindName("grid_coTheCoThai")));
            this.textblock_coTheCoThai = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_coTheCoThai")));
            this.grid_RungTrung = ((System.Windows.Controls.Grid)(this.FindName("grid_RungTrung")));
            this.textblock_RungTrung = ((System.Windows.Controls.TextBlock)(this.FindName("textblock_RungTrung")));
        }
    }
}


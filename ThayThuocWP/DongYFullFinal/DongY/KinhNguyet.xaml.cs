using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using System.Globalization;

namespace DongY
{
    public partial class KinhNguyet : PhoneApplicationPage
    {
        // Url of Home page
        private string MainUri = "/Libs/index.html";
        List<String> NumDayOfCircle = new List<string>();
        List<String> NumberKN = new List<string>();
        private bool Showed = false;
        private bool Changed = false;

        public KinhNguyet()
        {
            InitializeComponent();
            wbChart.Visibility = Visibility.Collapsed;
            for (int i = 20; i <= 40; i++)
            {
                NumDayOfCircle.Add(i.ToString());
            }
            for (int i = 3; i <= 8; i++)
            {
                NumberKN.Add(i.ToString());
            }
            listpicker_chuKiKinh.ItemsSource = NumDayOfCircle;
            listpicker_chuKiKinh.SelectedIndex = 8;//28 days
            listpicker_soNgayCoKinh.ItemsSource = NumberKN;
            listpicker_soNgayCoKinh.SelectedIndex = 1; //4 days

            datepicker_ngayDau.ValueChanged += datepicker_ngayDau_ValueChanged;
            listpicker_chuKiKinh.SelectionChanged += listpicker_chuKiKinh_SelectionChanged;
            listpicker_soNgayCoKinh.SelectionChanged += listpicker_soNgayCoKinh_SelectionChanged;
        }

        void listpicker_soNgayCoKinh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Changed = true;
        }

        void listpicker_chuKiKinh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Changed = true;
        }

        void datepicker_ngayDau_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            Changed = true;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (Changed == true)
            {
                Changed = false;
                return;
            }
            string id = NavigationContext.QueryString["id"];
            if (id == "-1")
            {
                if ((Application.Current as App).KN_BeginDate != null && (Application.Current as App).KN_Circle != 0)
                {
                    ShowChart((Application.Current as App).KN_BeginDate, (Application.Current as App).KN_Circle, (Application.Current as App).KN_Number);
                    datepicker_ngayDau.Value = (Application.Current as App).KN_BeginDate;
                    listpicker_chuKiKinh.SelectedIndex = (Application.Current as App).KN_Circle - 20;
                    listpicker_soNgayCoKinh.SelectedIndex = (Application.Current as App).KN_Number - 3;
                }
                return;
            }
            string strSelect = string.Format("SELECT * FROM Cycle WHERE id = {0}", id);

           
            try
            {
                Cycle ShowCycle = (Application.Current as App).dp.SelectList<Cycle>(strSelect)[0];
                DateTime BeginDate = DateTime.Parse(ShowCycle.ngaybatdau);
                datepicker_ngayDau.Value = BeginDate;
                listpicker_chuKiKinh.SelectedIndex = Int32.Parse(ShowCycle.chuky) - 20;
                listpicker_soNgayCoKinh.SelectedIndex = Int32.Parse(ShowCycle.songaykinh) - 3;

                int Circle = Int32.Parse(listpicker_chuKiKinh.SelectedItem.ToString());
                int Number = Int32.Parse(listpicker_soNgayCoKinh.SelectedItem.ToString());

                ShowChart(BeginDate, Circle, Number);
            }
            catch (Exception)
            {
                MessageBox.Show("Load thất bại, vui lòng thử lại!");
            }
        }

        private void LoadInfoDatabase()
        {
            NavigationService.Navigate(new Uri("/KinhNguyet_Load.xaml", UriKind.Relative));
        }

        private void SaveInfoToDatabase()
        {
            try
            {
                DateTime BeginDate = (DateTime)datepicker_ngayDau.Value;
                int Circle = Int32.Parse(listpicker_chuKiKinh.SelectedItem.ToString());
                int Number = Int32.Parse(listpicker_soNgayCoKinh.SelectedItem.ToString());

                Cycle NewCycle = new Cycle();
                NewCycle.id = "NULL";
                NewCycle.chuky = Circle.ToString();
                NewCycle.songaykinh = Number.ToString();
                NewCycle.ngaybatdau = BeginDate.ToShortDateString();
                NewCycle.thoigian = DateTime.Now.ToString("g", DateTimeFormatInfo.InvariantInfo);
                
                string strInsert = string.Format("INSERT INTO Cycle(id, chuky, songaykinh, ngaybatdau, thoigian) VALUES (NULL, '{0}', '{1}', '{2}', '{3}')", NewCycle.chuky, NewCycle.songaykinh, NewCycle.ngaybatdau, NewCycle.thoigian);

                //string strInsert = string.Format("INSERT INTO Cycle(id) VALUES ('{0}')", NewCycle.id);

                (Application.Current as App).dp.Insert<Cycle>(strInsert);
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
        }

        private async void ShowChart(DateTime BeginDate, int Circle, int Number)
        {
            await WriteToFile(GetChart(BeginDate, Circle, Number));

            wbChart.Navigate(new Uri(MainUri, UriKind.Relative));
            wbChart.IsScriptEnabled = true;
            wbChart.Visibility = Visibility.Visible;
            (Application.Current as App).KN_BeginDate = BeginDate;
            (Application.Current as App).KN_Circle = Circle;
            (Application.Current as App).KN_Number = Number;
        }

        private void button_xemLich_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                DateTime BeginDate = (DateTime)datepicker_ngayDau.Value;
                int Circle = Int32.Parse(listpicker_chuKiKinh.SelectedItem.ToString());
                int Number = Int32.Parse(listpicker_soNgayCoKinh.SelectedItem.ToString());

                ShowChart(BeginDate, Circle, Number);
                Showed = true;
                
            }
            catch(Exception)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
        }


        private void Browser_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            MessageBox.Show("Không thể hiển thị biểu đồ!");
        }

        private String GetChart(DateTime BeginDate, int Circle, int Number)
        {
            String result = "";
            int BegDay = BeginDate.Day;
            int BegMon = BeginDate.Month;
            
            DateTime EndDate = BeginDate.AddDays(Circle);
            int EndDay = EndDate.Day;
            int EndMon = EndDate.Month;
            int EndYear = EndDate.Year;

            result += 
        "<html>" +
            "<head>" +
                //"<meta name=\"viewport\" content=\"width=device-width; user-scaleable=no; initial-scale=1.0; />" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
                "<meta charset='Windows-1257'>"+
                "<meta name='viewport' content='width=device-width' />"+
                "<meta name='viewport' content='height=device-height' />" +
                "<meta name='viewport' content='user-scalable=no' />"+
                "<meta name='viewport' content='initial-scale = 1.0' />"+
                "<meta name='HandheldFriendly' content='true' />"+
                //"<link rel=\"stylesheet\" type=\"text/css\" href=\"/html/css/phone.css\" />" +
                "<title>Windows Phone</title>" +
                
                "<script>function getDocHeight() {" +
                    "return document.getElementById('pageWrapper').offsetHeight;}" +
                "function SendDataToPhoneApp() {"+
                    "window.external.Notify('' + getDocHeight());" +
                "}</script> " +
            "</head>" +
            "<body  style=\"margin:0px;padding:0px;height:250px;\" background =\"../Assets/background_320wx416h.png\" onLoad=\"SendDataToPhoneApp()\" >" +
                "<center>" +
                "<div id=\"chart\" style=\"zoom:60%;height:250px;\" >" +
                    "<table border=\"1\" style=\"text-align:center\">" +
                    "<tr>" +
                      "";

            if (BegMon == EndMon)
            {
                result += "<td id=\"month_1\" colspan=\""+ (Circle).ToString() +"\">Tháng " + BegMon.ToString() + "</td>";
            }
            else
            {
                List<int> Month = new List<int>();
                while ((EndMon - BegMon) >= 0)
                {
                    Month.Add(BegMon);
                    BegMon++;
                }
                
                int sday= BegDay;
                for (int i = 0; i < Month.Count - 1; i++)
                {
                    int j = ((DateTime.DaysInMonth(EndYear, Month[i]) - sday + 1));
                    sday = 1;
                    result += "<td id=\"month_1\" colspan=\""+ j.ToString() +"\">Tháng " + Month[i].ToString() + "</td>";
                }
                result += "<td id=\"month_1\" colspan=\"" + (Circle - sday + 1).ToString() + "\">Tháng " + Month[Month.Count - 1].ToString() + "</td>";
                
            }
            
            result += "</tr>" +
                     "<tr width=\"50\">" +
                      "";

            DateTime Begin = BeginDate.AddDays(0);
            for (int i = 0; i < Circle; i++)
            {
                result += ("<td id=\"day_" + (i + 1).ToString() + "\" width=\" 15\">" + Begin.Day.ToString() + "</td>");
                //result += ("<td id=\"day_" + (i + 1).ToString() + "\" width=\" 15\">" + i.ToString() + "</td>");
                Begin = Begin.AddDays(1);
            }

            result += "</tr><tr>";
            result += GetChart_Free(BeginDate, Circle, Number);
            //result += GetChart_Free(EndDate.AddDays(1), Circle, Number);
            
            result += "</tr><tr>";
            result += GetChart_Pregnant(BeginDate, Circle, Number);
            //result += GetChart_Pregnant(EndDate.AddDays(1), Circle, Number);
            
            result += "</tr></table></div></center></body></html>";

            return result;
        }

        private String GetChart_Free(DateTime BeginDate, int Circle, int Number)
        {
            String result = "";
            int BegDay = BeginDate.Day;
            int BegMon = BeginDate.Month;
            
            DateTime StartDate = BeginDate.AddDays(Circle - 18);
            int StartDay = Circle - 17;
            int StartMon = StartDate.Month;
            

            BeginDate = BeginDate.AddDays(Circle);
            int EndDay = BeginDate.Day;
            int EndMon = BeginDate.Month;

            
            for (int i = 0; i < Number; i++)
            {
                result += "<td height=\"100\"><div id=\"qh_"+(i+1).ToString()+"\" style=\"height:100px; width: 15px;background: url(../Assets/images/par.jpg) -45px 0px; \"></div></td>";
            }

            for (int i = Number + 1; i < StartDay; i++)
            {
                result += "<td height=\"100\"><div id=\"qh_" + (i + 1).ToString() + "\" style=\"height:100px; width: 15px;background: url(../Assets/images/par.jpg) 0px 0px; \"></div></td>";
            }

            {
                if (Number - StartDay >= 5)
                { 
                }
                else if (Number - StartDay >= 4)
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";
                }
                else if (Number - StartDay >= 3)
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 100px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";
                }
                else if (Number - StartDay >= 2)
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 90px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 100px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";
                }
                else if (Number - StartDay >= 1)
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 60px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 90px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 100px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";

                }
                else if (Number - StartDay >= 0)
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 20px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 60px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 90px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 100px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";
                }
                else
                {
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 10px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 20px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 60px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 90px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 100px; \"></div></td>";
                    result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 80px; \"></div></td>";
                } 
                
                result += "<td height=\"100\"><div id=\"qh_" + (StartDay++).ToString() + "\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 0px 50px; \"></div></td>";
            }

            for (int i = Circle; i >= Circle - 10; i--)
            {
                result += "<td height=\"100\"><div id=\"qh_" + (i + 1).ToString() + "\" style=\"height:100px; width: 15px;background: url(../Assets/images/par.jpg) 0px 0px; \"></div></td>";
            }

            return result;
        }

        private String GetChart_Pregnant(DateTime BeginDate, int Circle, int Number)
        {
            String result = "";
            int BegDay = BeginDate.Day;
            int BegMon = BeginDate.Month;

            DateTime StartDate = BeginDate.AddDays(Circle - 18);
            int StartDay = Circle - 17;
            int StartMon = StartDate.Month;


            BeginDate = BeginDate.AddDays(Circle);
            int EndDay = BeginDate.Day;
            int EndMon = BeginDate.Month;


            for (int i = 0; i < StartDay - 1; i++)
            {
                result += "<td><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg)30px 100px;\"></div></td>";
            }

            {
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 30px 90px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 30px 80px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 30px 40px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 30px 20px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 45px 10px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 45px 30px;\"></div></td>";
                result += "<td height=\"100\"><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg) 30px 50px;\"></div></td>";
            }
            
            for (int i = Circle; i >= StartDay + 7; i--)
            {
                result += "<td><div id=\"kn_1\" style=\"height:100px; width: 15px;background:url(../Assets/images/par.jpg)30px 100px;\"></div></td>";
            }
            return result;
        }

        private async Task WriteToFile(String X)
        {
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(X.ToCharArray());

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("Libs",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("index.html",
            CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (Showed)
            {
                if(MessageBox.Show("Thực sự bạn muốn lưu lại?","", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    SaveInfoToDatabase();
            }
            else
            {
                MessageBox.Show("Bạn phải xem lịch trước khi lưu!");
            }
        }

        private void button_load_Click(object sender, RoutedEventArgs e)
        {
            if (Showed)
            {
                if (MessageBox.Show("Bạn muốn rời trang hiện tại?", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return;
            }
            LoadInfoDatabase();
        }
    }
}
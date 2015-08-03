using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DongY
{
    public partial class bmi : PhoneApplicationPage
    {
        private bool g_bChange = false;

        List<BMI> _listBMI;

        int tuoi_index = 0;
        int gioitinh_index = 0;
        int dotuoi_index = 0;
        

        String[] strGioiTinh = { "Nam",
                                 "Nữ",
                                };
        String[] strDoTuoi = { "Trưởng Thành",
                                "Trẻ Em",
                             };
        String[] strTuoi = { "5","6","7","8","9","10","11","12","13","14","15","16","17","18","19"
                             };

        public bmi()
        {
            InitializeComponent();
            this.listpicker_doTuoi.ItemsSource = strDoTuoi;
            this.listpicker_gioiTinh.ItemsSource = strGioiTinh;
            this.listpicker_tuoi.ItemsSource = strTuoi;
            loadData();
        }

        private void loadData()
        {
            string strSelect = "SELECT * FROM BMI";
            _listBMI = (Application.Current as App).dp.SelectList<BMI>(strSelect);
        }

        //Độ tuổi selection changed
        private void listpicker_doTuoi_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.listpicker_doTuoi.SelectedItem.ToString() == "Trưởng Thành")
            {
                this.listpicker_tuoi.Width = 0;
                this.listpicker_tuoi.Opacity = 0;

            }
            else //Choosed "Nữ"
            {
                this.listpicker_tuoi.Width = 100;
                this.listpicker_tuoi.Opacity = 150;

            }

            try
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                {
                    if (this.listpicker_doTuoi.SelectedItem != null)
                    {
                        var selectedItem = (sender as ListPicker).SelectedItem;
                        dotuoi_index = listpicker_doTuoi.SelectedIndex; //just for testing

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Gioi Tinh selection changed
        private void listpicker_gioiTinh_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            g_bChange = true;
            try
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                {
                    if (this.listpicker_gioiTinh.SelectedItem != null)
                    {
                        var selectedItem = (sender as ListPicker).SelectedItem;
                        gioitinh_index = listpicker_gioiTinh.SelectedIndex; //just for testing

                    }
                }
            }
            catch (Exception ex)
            {
                g_bChange = false;
                MessageBox.Show(ex.Message);
            }
        }

        //Tuổi selection changed
        private void listpicker_tuoi_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            g_bChange = true;
            try
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                {
                    if (this.listpicker_tuoi.SelectedItem != null)
                    {
                        var selectedItem = (sender as ListPicker).SelectedItem;
                        tuoi_index = listpicker_tuoi.SelectedIndex; //just for testing

                    }
                }
            }
            catch (Exception ex)
            {
                g_bChange = false;
                MessageBox.Show(ex.Message);
            }
        }

        //Tính BMI button taped
        private void button_caculate_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string strHeight = this.textbox_chieuCao.Text;
            string strWeight = this.textbox_canNang.Text;

            //Check empty
            if (strWeight == "")
            {
                MessageBox.Show("\"Cân Nặng\" không được để trống, vui lòng nhập vào.");
                
            }
            else if (strHeight == "")
            {
                MessageBox.Show("\"Chiều Cao\" không được để trống, vui lòng nhập vào.");
            }
            else
            try
            {

                //Check == 0
                if (Int32.Parse(strWeight) == 0)
                {
                    MessageBox.Show("\"Cân Nặng\" không được bằng 0, vui lòng nhập lại.");
                }
                else if (Int32.Parse(strHeight) == 0)
                {
                    MessageBox.Show("\"Chiều Cao\" không được bằng 0, vui lòng nhập lại.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng nhập thông tin để tính!");
            }
        }

        

        private void button_caculate_Click_1(object sender, RoutedEventArgs e)
        {
            double can_nang = 1;
            double chieu_cao = 1;
            try
            {
                if (Convert.ToDouble(textbox_chieuCao.Text) != 0)
                {
                    can_nang = Convert.ToDouble(textbox_canNang.Text);
                    chieu_cao = Convert.ToDouble(textbox_chieuCao.Text);
                }
            }
            catch (Exception)
            {
                return;
            }

            double d_bmi = can_nang / (chieu_cao * chieu_cao) * 10000;
            d_bmi = Math.Round(d_bmi, 2);

            textbox_tinhBmi.Text = Convert.ToString(d_bmi);


            string result = "";

            double chieu_cao_m = chieu_cao / 100;
            double min_recommend, max_recommend;
            if (dotuoi_index == 0) // trưởng thành
            {
                if (gioitinh_index == 0) // nam
                {
                    min_recommend = Math.Round((20 * chieu_cao_m * chieu_cao_m),1);
                    max_recommend = Math.Round((25 * chieu_cao_m * chieu_cao_m),1);
                }
                else // nữ
                {
                    min_recommend = Math.Round((18 * chieu_cao_m * chieu_cao_m),1);
                    max_recommend = Math.Round((23 * chieu_cao_m * chieu_cao_m),1);
                }
                
                if (18.5 > d_bmi)
                    result = "Bạn gầy\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
                else if (18.5 <= d_bmi && d_bmi < 25)
                    result = "Bạn bình thường";
                else if (25 <= d_bmi && d_bmi < 30)
                    result = "Bạn thừa cân\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
                else if (30 <= d_bmi)
                    result = "Bạn béo phì\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
            }
            else // trẻ em
            {
                int index = 0;
                

                if (gioitinh_index == 0)
                {
                    index = tuoi_index;
                }
                else
                {
                    index = tuoi_index + 15;
                }

                string gender = _listBMI[index].GenderId;
                string old = _listBMI[index].Old;
                double sd3a = Convert.ToDouble(_listBMI[index].sd3a);
                double sd2a = Convert.ToDouble(_listBMI[index].sd2a);
                double sd1 = Convert.ToDouble(_listBMI[index].sd1);
                double sd2 = Convert.ToDouble(_listBMI[index].sd2);
                double sd3 = Convert.ToDouble(_listBMI[index].sd3);

                min_recommend = Math.Round((sd2a * chieu_cao_m * chieu_cao_m), 1);
                max_recommend = Math.Round((sd1 * chieu_cao_m * chieu_cao_m), 1);

                if (sd3a > d_bmi)
                    result = "Bạn rất gầy\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
                else if (sd2a > d_bmi && d_bmi >= sd3a)
                    result = "Bạn gầy\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
                else if (sd1 > d_bmi && d_bmi >= sd2a)
                    result = "Bạn bình thường";
                else if (sd2 > d_bmi && d_bmi >= sd1)
                    result = "Bạn thừa cân\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";
                else if (sd2 <= d_bmi)
                    result = "Bạn béo phì\nBạn nên giữ mức cân nặng từ " + min_recommend.ToString() + " kg đến " + max_recommend.ToString() + " kg";

                //test = Convert.ToString(index) + " - " + old + " - " + gender;
            }


            textbox_noiDung.Text = result;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (g_bChange || listpicker_doTuoi.SelectedItem.ToString() != (Application.Current as App).lpk_dotuoi
                || listpicker_gioiTinh.SelectedItem.ToString() != (Application.Current as App).lpk_gioitinh
                || listpicker_tuoi.SelectedItem.ToString() != (Application.Current as App).lpk_tuoi)
            {
                g_bChange = false;
                return;
            }
            this.listpicker_gioiTinh.SelectedItem = "Nam";
            this.listpicker_doTuoi.SelectedItem = "Trưởng Thành";
            this.listpicker_tuoi.Width = 0;
            this.listpicker_tuoi.Opacity = 0;
        }


    }
}
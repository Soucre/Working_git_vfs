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
using Microsoft.Phone.Tasks;

namespace DongY
{
    public partial class TuVan : PhoneApplicationPage
    {
        public TuVan()
        {
            InitializeComponent();
        }


        private bool NameFirst = true;
        private bool AddrFirst = true;
        private bool EmailFirst = true;
        private bool PhoneFirst = true;
        private bool TitleFirst = true;
        private bool ContentFirst = true;

        private void textbox_hoVaTen_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (NameFirst)
            {
                NameFirst = false;
                textbox_hoVaTen.Text = "";
            }
        }

        private void textbox_hoVaTen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!NameFirst && textbox_hoVaTen.Text == "")
            {
                textbox_hoVaTen.Text = "Họ và tên";
                NameFirst = true;
            }
        }


        private void button_email_Click_1(object sender, RoutedEventArgs e)
        {
            if (NameFirst)
            {
                MessageBox.Show("Hãy cho chúng tôi biết tên của bạn!");
                return;
            }
            if (ContentFirst)
            {
                MessageBox.Show("Hãy cho chúng tôi biết Bạn muốn nhắn gửi gì ở phần nội dung?");
                return;
            }
            if (PhoneFirst)
            {
                if (MessageBox.Show("Bạn có đồng ý gửi số điện thoại để có thể liên lạc lại với bạn dễ hơn?", "", MessageBoxButton.OKCancel) == MessageBoxResult.Yes)
                    return;
            }
            if (TitleFirst)
            {
                if (MessageBox.Show("Email của bạn sẽ được ưu tiên hơn nếu có tiêu đề rõ ràng, bạn có muốn sửa lại nó?", "", MessageBoxButton.OKCancel) == MessageBoxResult.Yes)
                    return;
            }
            if (EmailFirst)
            {
                MessageBox.Show("Hãy cho chúng tôi biết email của bạn!");
                return;
            }

            string hoten = textbox_hoVaTen.Text;
            string diachi = textbox_diaChi.Text;
            string noidung = textbox_noiDung.Text;
            string dienthoai = textbox_soDienThoai.Text;
            string tieude = textbox_chuDe.Text;
            string email = textbox_email.Text;

            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = tieude;
            emailComposeTask.Body = "họ tên: " + hoten + "\n" +
                                    "địa chỉ: " + diachi + "\n" +
                                    "điện thoại: " + dienthoai + "\n" +
                                    "nội dung: " + noidung;
            emailComposeTask.To = email;
            emailComposeTask.Cc = "nguyenxuangieng720@live.com";

            emailComposeTask.Show();
        }

        private void textbox_diaChi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AddrFirst)
            {
                AddrFirst = false;
                textbox_diaChi.Text = "";
            }
        }

        private void textbox_diaChi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!AddrFirst && textbox_diaChi.Text == "")
            {
                textbox_diaChi.Text = "Địa chỉ";
                AddrFirst = true;
            }
        }

        private void textbox_email_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (EmailFirst)
            {
                EmailFirst = false;
                textbox_email.Text = "";
            }
        }

        private void textbox_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!EmailFirst && textbox_email.Text == "")
            {
                textbox_email.Text = "Email";
                EmailFirst = true;
            }
        }

        private void textbox_soDienThoai_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (PhoneFirst)
            {
                PhoneFirst = false;
                textbox_soDienThoai.Text = "";
            }
        }

        private void textbox_soDienThoai_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!PhoneFirst && textbox_soDienThoai.Text == "")
            {
                textbox_soDienThoai.Text = "Số điện thoại";
                PhoneFirst = true;
            }
        }

        private void textbox_chuDe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (TitleFirst)
            {
                TitleFirst = false;
                textbox_chuDe.Text = "";
            }
        }

        private void textbox_chuDe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TitleFirst && textbox_chuDe.Text == "")
            {
                textbox_chuDe.Text = "Chủ đề";
                TitleFirst = true;
            }
        }

        private void textbox_noiDung_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ContentFirst)
            {
                ContentFirst = false;
                textbox_noiDung.Text = "";
            }
        }

        private void textbox_noiDung_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!ContentFirst && textbox_noiDung.Text == "")
            {
                textbox_noiDung.Text = "Nội dung";
                ContentFirst = true;
            }
        }


    }
}
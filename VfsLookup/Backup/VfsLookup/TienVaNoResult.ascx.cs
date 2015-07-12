using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VfsLookup
{
    public partial class TienVaNoResult : System.Web.UI.UserControl
    {
        public DateTime DateTn;
        public DateTime DateDn;
        public string TenTk;
        public Libs.StockTienNoReport report;
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request["type"])
            {
                case "Y":
                    this.lbTypeCode.Text = "Y";
                    this.lbTypeText.Text = "Tổng hợp tài khoản";

                    break;
                default:
                    this.lbTypeCode.Text = "N";
                    this.lbTypeText.Text = "Tổng hợp tài khoản";
                    break;
            }
        }

        internal void loadContent()
        {
            this.txtTkMa.Text = this.report.MaTk;
            this.txtTKName.Text = this.report.TenTaiKhoan;
            this.txtDateTn.Text = String.Format("{0:dd-MM-yyyy}", this.DateTn);
            this.txtDateDn.Text = String.Format("{0:dd-MM-yyyy}", this.DateDn);
            
            this.report.makeReport(this.DateTn, this.DateDn);
            this.tblTienNhan.DataSource = this.report.TienNhan;
            this.tblTienNhan.DataBind();

            this.tblTienChi.DataSource = this.report.TienChi;
            this.tblTienChi.DataBind();

            this.tblTienCho.DataSource = this.report.TienCho;
            this.tblTienCho.DataBind();
            //throw new NotImplementedException();
            this.lbTinhTrangDauKy.Text = String.Format("{0:#,##0}", this.report.tongDauKy());
            this.lbTinhTrangCuoiKy.Text = String.Format("{0:#,##0}", this.report.tongCuoiKy());
            this.lbTienNhanDuocTrongKy.Text = String.Format("{0:#,##0}", this.report.tongNhan());
            this.lbTienChiRaTrongKY.Text = String.Format("{0:#,##0}", this.report.tongChi());
            this.lbTienChoVeCuoiKy.Text = String.Format("{0:#,##0}", this.report.tongCho());
        }
        Boolean[] tmp = new Boolean[] { true, true, true, true, true, true, true };
        protected string getClass(int group)
        {
            tmp[group] = !tmp[group];
            if (tmp[group]) return "odd";
            return "even";
        }
    }
}
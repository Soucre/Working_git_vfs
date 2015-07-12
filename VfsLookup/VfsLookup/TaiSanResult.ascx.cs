using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VfsLookup.Libs;
using System.Data;

namespace VfsLookup
{
    public partial class TaiSanResult : System.Web.UI.UserControl
    {
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
        public void loadContent()
        {
            //this.txtTKName.Text = "Bùi Đức Tuyển";
            this.MaTK = this.report.MaTk;
            this.txtDateTn.Text = String.Format("{0:dd-MM-yyyy}",this.DateTn);
            this.txtDateDn.Text = String.Format("{0:dd-MM-yyyy}", this.DateDn);
            this.report.dailyReport(this.DateTn, this.DateDn);
            tbTaiSan.DataSource = this.report.TaiSan;
            tbTaiSan.DataBind();
            tbNo.DataSource = this.report.No;
            tbNo.DataBind();
            tbTaiSanKhac.DataSource = this.report.TaiSanKhac;
            tbTaiSanKhac.DataBind();
            this.lbTaiSanTn.Text = String.Format("{0:#,##0}", this.report.tongTaiSanTN());
            this.lbTaiSanDn.Text = String.Format("{0:#,##0}", this.report.tongTaiSanDN());
            this.lbNoTn.Text = String.Format("{0:#,##0}", this.report.tongNoTN());
            this.lbNoDn.Text = String.Format("{0:#,##0}", this.report.tongNoDN());
            this.lbTaiSanKhacTn.Text = String.Format("{0:#,##0}", this.report.tongTaiSanKhacTN());
            this.lbTaiSanKhacDn.Text = String.Format("{0:#,##0}", this.report.tongTaiSanKhacDN());
            this.lbTaiSanRongTn.Text = String.Format("{0:#,##0}", this.report.taiSanRongTN());
            this.lbTaiSanRongDn.Text = String.Format("{0:#,##0}", this.report.taiSanRongDN());

            tblYearly=this.report.getDataForAYear(DateTime.Now);
            tbReportYearly.DataSource = tblYearly;
            tbReportYearly.DataBind();
            

        }
        DataTable tblYearly;
        public StockTaiSanReport report ;
        string tenTk = "";

        public string TenTk
        {
            get { return tenTk; }
            set {
                this.txtTKName.Text = value;
                tenTk = value; }
        }

        String maTK = "";
       // public bool builreport=true;
        
        public String MaTK
        {
            get { return maTK; }
            set {
                this.txtTkMa.Text = value;
                maTK = value; }
        }


        public DateTime DateTn { get; set; }

        public DateTime DateDn { get; set; }
        Boolean[] tmp = new Boolean[] {true,true,true,true,true,true,true };
        protected string getClass(int group)
        {
            tmp[group] = !tmp[group];
            if (tmp[group]) return "odd";
            return "even";
        }
        protected string getCharTitle()
        {
            if (this.tblYearly == null) return "";
            List<String> result = new List<string>();
            foreach (DataRow r in this.tblYearly.Rows)
            {
                //int tsr = (int)int.Parse(r["ten"].ToString()) / 1000000;
                result.Add("\"" + r["ten"] + "\"");

            }
            return String.Join(",", result.ToArray());
        }
        protected string getCharValue()
        {
            if (this.tblYearly == null) return "";
            List<String> result = new List<string>();
            foreach (DataRow r in this.tblYearly.Rows)
            {
                double tsr = double.Parse(r["taisanrong"].ToString()) / 1000000;
                result.Add( "" + tsr + "");
                
            }
           return String.Join(",",result.ToArray());
        }
    }
}
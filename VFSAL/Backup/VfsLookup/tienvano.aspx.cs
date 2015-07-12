using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using VfsLookup.Libs;
using System.Data;

namespace VfsLookup
{
    public partial class tienvano : System.Web.UI.Page
    {
        DateTime dateTn = new DateTime();
        DateTime dateDn = new DateTime();
        protected string maTk = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }

            this.ctr_tienvano_result.Visible = false;


            if (Request["acccode"] != null)
            {
                this.maTk = Request["acccode"];
                //DateTime time=new DateTime(2013,12,10);

                if (Request["txtDateTn"] != null)
                {
                    try
                    {
                        string datetn = Request["txtDateTn"];
                        dateTn = DateTime.ParseExact(datetn, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch (Exception ex)
                    {

                        //this.ctr_taisan_result.builreport = false;
                        this.txtMessageError.Text = "Ngày nhập vào không hợp lệ";
                        return;
                    }
                }
                if (Request["txtDateDn"] != null)
                {
                    try
                    {
                        string datedn = Request["txtDateDn"];
                        dateDn = DateTime.ParseExact(datedn, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch (Exception ex)
                    {
                        //this.ctr_taisan_result.builreport = false;
                        this.txtMessageError.Text = "Ngày nhập vào không hợp lệ";
                        return;
                    }
                }
                if (this.dateTn > this.dateDn)
                {
                    this.txtMessageError.Text = "Ngày cuối kỳ phải lớn hơn ngày đầu kỳ";
                    return;
                }
                
                string query = "SELECT * FROM [dbo].[TradingAccount]  WHERE [CustomerId] = '" + Session["username"].ToString().Replace("'", "\\'") + "' and AccountId = '" + Request["acccode"] + "'";
                DataTable tbl = VSDBConnection.getDataTable(query, VSDBConnection.CSVSFServices);
                if (tbl.Rows.Count == 0)
                {
                    this.txtMessageError.Text = "Tài khoản không hợp lệ!";
                    return;
                }
                //DateTime d=Libs.VSDateTime.getPreBusinessDay(time, 3); 
                string type = "N";
                if (Request["type"] == "Y") type = "Y";
                StockTienNoReport no = new StockTienNoReport(Request["acccode"], type);

                //this.ctr_taisan_result.MaTK = "";

                this.ctr_tienvano_result.TenTk = no.TenTaiKhoan;
                if (!no.exisAcc)
                {
                    this.txtMessageError.Text = "Không tìm thấy tài khoản";
                    return;
                }
                this.ctr_tienvano_result.report = no;
                //if (this.ctr_taisan_result.builreport)
                //{
                this.ctr_tienvano_result.DateTn = dateTn;
                this.ctr_tienvano_result.DateDn = dateDn;
                this.ctr_tienvano_result.loadContent();
                //}
                //ts.dailyReport(new DateTime(2013, 8, 31), new DateTime(2013, 9, 30));

                //GridView1.DataSource = ts.TaiSan;
                //GridView1.DataBind();
                //GridView2.DataSource = ts.No;
                //GridView2.DataBind();
                //GridView3.DataSource = ts.TaiSanKhac;
                //GridView3.DataBind();
                //this.lbTSRongTN.Text = ts.taiSanRongTN().ToString();
                //this.lbTSRongDN.Text = ts.taiSanRongDN().ToString();
                //GridView4.DataSource = ts.getDataForAYear(new DateTime(2013, 11, 6));
                //GridView4.DataBind();


                this.ctr_tienvano_result.Visible = true;
            }
        }
        protected string getAccOption(string selected)
        {
            string option = "";
            string query = "SELECT * FROM [dbo].[TradingAccount]  WHERE [CustomerId] = '" + Session["username"].ToString().Replace("'", "\\'") + "'";
            DataTable tbl = VSDBConnection.getDataTable(query, VSDBConnection.CSVSFServices);
            foreach (DataRow r in tbl.Rows)
            {
                if (r["AccountId"].ToString() == selected)
                {
                    option += "<option value='" + r["AccountId"].ToString() + "' selected='selected'>" + r["AccountId"].ToString() + "</option>";
                }
                else
                {
                    option += "<option value='" + r["AccountId"].ToString() + "' >" + r["AccountId"].ToString() + "</option>";
                }
            }
            return option;
        }
    }
}
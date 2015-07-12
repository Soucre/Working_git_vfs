using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VfsLookup.Libs;

namespace VfsLookup
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime time=new DateTime(2013,12,10);

            //DateTime d=Libs.VSDateTime.getPreBusinessDay(time, 3); 
            StockTaiSanReport ts = new StockTaiSanReport("094K002555","N");
            this.lbName.Text=ts.TenTaiKhoan;
            ts.dailyReport(new DateTime(2013, 8,31), new DateTime(2013, 9, 30));
            GridView1.DataSource = ts.TaiSan;
            GridView1.DataBind();
            GridView2.DataSource = ts.No;
            GridView2.DataBind();
            GridView3.DataSource = ts.TaiSanKhac;
            GridView3.DataBind();
            this.lbTSRongTN.Text = ts.taiSanRongTN().ToString();
            this.lbTSRongDN.Text = ts.taiSanRongDN().ToString();
            GridView4.DataSource = ts.getDataForAYear(new DateTime(2013,11,6)) ;
            GridView4.DataBind();

        }
    }
}
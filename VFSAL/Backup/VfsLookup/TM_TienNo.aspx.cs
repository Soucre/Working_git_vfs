using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VfsLookup.Libs;

namespace VfsLookup
{
    public partial class TM_TienNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StockTienNoReport report = new StockTienNoReport("094K004747","N");
            report.makeReport(new DateTime(2013, 10, 06), new DateTime(2013, 10, 27));
            this.GridViewNhan.DataSource = report.TienNhan;
            this.GridViewNhan.DataBind();
            this.GridViewChi.DataSource = report.TienChi;
            this.GridViewChi.DataBind();
            this.GridViewCho.DataSource = report.TienCho;
            this.GridViewCho.DataBind();
        }
    }
}
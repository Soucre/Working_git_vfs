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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["act"] == "logout")
            {
                   Session["username"] = null;
            }
            if (Session["username"] != null)
            {
                this.redirectDefault();
            }
        }

        protected void btnSubmitLogin_Click(object sender, EventArgs e)
        {
            string sql = "select * from Investor where customerId='" + txt_username.Text.Replace("\'", "\\'") + "' and password='" + VSDBConnection.MD5Hash(txt_password.Text) + "'";
            DataTable tbl=VSDBConnection.getDataTable(sql, VSDBConnection.CSOnlineTrading);
            if (tbl.Rows.Count > 0)
            {
                Session["username"] = tbl.Rows[0]["customerId"];
                this.redirectDefault();
            }
            else
            {
                lbMessage.Text = "Sai mật khẩu, vui lòng đăng nhập lại";
                return;
            }
            

        }
        protected void redirectDefault()
        {
            Response.Redirect("taisan.aspx");
        }
    }
}
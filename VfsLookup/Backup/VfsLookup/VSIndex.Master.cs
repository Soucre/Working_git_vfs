using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VfsLookup
{
    public partial class VSIndex : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["username"] != null)
            {
                hlLogin.NavigateUrl = "login.aspx?act=logout";
                hlLogin.Text = "Thoát";
            }
            else
            {
                hlLogin.NavigateUrl = "login.aspx";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
    }

    protected override void OnInit(EventArgs e)
    {
        //RadioButtonList fdas = new RadioButtonList();
        //fdas.SelectedValue
        rbtLstTimeView.SelectedValue = "8";
    }
    /// <summary>
    /// Tạo báo cáo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateReport_Click(object sender, EventArgs e)
    {

    }
}
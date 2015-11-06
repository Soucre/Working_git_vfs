using DAOvEntitiesFramwork_CusServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ReportList : System.Web.UI.Page
{

    ReportDAO reDAO = new ReportDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            int output = 0;
            //reDAO.GetListReport(ReportEnums.CreateDate, "DESC", 1, 10, out output);
            LoadListReport(1);
        }
        
    }

    private void LoadListReport(int page)
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = reDAO.GetListReport(ReportEnums.CreateDate, "DESC", page, 20, out totalRow);
        this.RepeaterData.DataBind();

        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;
    }
    
    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.LoadListReport(Convert.ToInt32(e.CommandArgument));
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.LoadListReport(Convert.ToInt32(e.CommandArgument));
    }

    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
     
    }
    protected void RepeaterData_OnItemDataBound(object ob, RepeaterItemEventArgs e)
    {
        
    }

    protected object GetOrderDirectionIndicator(string property)
    {
        //if (property.Equals(orderByCustomerAccount.ToString()))
        //{
        //    return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectionCustomerAccount);
        //}
        //else
            return "";
    }
    
}
using DAOvEntitiesFramwork_CusServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ReportCustomer : System.Web.UI.Page
{

    CustomerLogDAO customerLogDAO = new CustomerLogDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLogReport();
        }
    }

    protected void LoadLogReport()
    {
        this.RepeaterData.DataSource =  customerLogDAO.getListCustomerVIPType();
        this.RepeaterData.DataBind();
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
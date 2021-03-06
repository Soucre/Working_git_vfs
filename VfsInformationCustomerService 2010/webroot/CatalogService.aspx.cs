using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vfs.WebCrawler.Utility;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

public partial class CustomerService_CatalogService : System.Web.UI.Page
{
    int serviceTypeId;    
    string action;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.paramater();
            this.UpdateInterface();
        }
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);
        if (this.action == "modify")
        {
            this.serviceTypeId = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_SERVICE_TYPE_ID));
        }
    }

    private void UpdateInterface()
    {
        int totalRow;
        this.RepeaterMessageContent.DataSource = ServiceTypeService.GetServiceTypeList(ServiceTypeColumns.ServiceTypeDescription, "ASC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterMessageContent.DataBind();
        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;
    }
    protected void InsertMessageContent_Click(object ob, EventArgs e)
    {
        Response.Redirect("CatalogServiceDetail.aspx?action=new");
    }        
    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }
    protected void RepeaterServiceType_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {       
        
        if (e.CommandName == "delete")
        {
            ServiceTypeService.DeleteServiceType(Convert.ToInt32(e.CommandArgument));            
        }
        this.UpdateInterface();
    }

    protected void RepeaterServiceType_OnItemDataBound(object ob,RepeaterItemEventArgs e)
    {
        RepeaterServiceTypeItem(e.Item);
    }

    private void RepeaterServiceTypeItem(RepeaterItem e)
    {
        
        if (e.ItemType == ListItemType.Item || e.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                ImageButton lbtnDelete = (ImageButton)e.FindControl("deleteImage");
                HtmlImage image = (HtmlImage)e.FindControl("imgDelete");

                //if (Common.ExistServiceTypeIdForMessageContent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true)
                if (Common.ExistsServiceTypeForContentTemplate(Convert.ToInt32(lbtnDelete.CommandArgument)) == true)
                    
                {
                    lbtnDelete.Enabled = false;                
                }
                else
                {
                    lbtnDelete.Enabled = true;
                    lbtnDelete.Attributes["onClick"] = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
                }
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message);
                //this.latestException = ex;
            }
        }
    }
    
}

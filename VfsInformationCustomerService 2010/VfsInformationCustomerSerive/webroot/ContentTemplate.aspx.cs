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

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using Vfs.WebCrawler.Utility;

public partial class CustomerService_ModelContent : System.Web.UI.Page
{
    int contentTemplateID;
    string action;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SetTextPageLoad();
        if (!IsPostBack)
        {
            this.paramater();            
            
        }
        this.UpdateInterface();
    }

    private void SetTextPageLoad()
    {
        this.Page.Title = Resources.UIResource.ContentTemplateListTitle;
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);
        if (this.action == "modify")
        {
            this.contentTemplateID = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_SERVICE_TYPE_ID));
        }
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

    private void UpdateInterface()
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.Description, "ASC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterData.DataBind();
        this.topPaging.PageSize = this.bottomPaging.PageSize = ApplicationHelper.PageSize;        
        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;        
    }
    protected void InsertButton_Click(object ob, EventArgs e)
    {
        Response.Redirect("ContentTeplateDetail.aspx?action=new");
    }
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            if (e.CommandArgument.Equals(ApplicationHelper.ContentemplateIdForPorfolioSMS) )
                return;
            if (Common.ExistsContentTemplateForMessageContent(Convert.ToInt32(e.CommandArgument)) == true || Common.ExistsContentTemplateForMessageContentSent(Convert.ToInt32(e.CommandArgument)) == true )
            {
                return;
            }
            else
            {
                ContentTemplateService.DeleteContentTemplateAndAttachement(Convert.ToInt32(e.CommandArgument));                
            }
        }

        this.UpdateInterface();
    }
    protected void RepeaterData_OnItemDataBound(object ob, RepeaterItemEventArgs e)
    {
        RepeaterItem(e.Item);
    }

    private void RepeaterItem(RepeaterItem e)
    {
        if (e.ItemType == ListItemType.Item || e.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                ImageButton lbtnDelete = (ImageButton)e.FindControl("deleteImage");
                HtmlImage image = (HtmlImage)e.FindControl("imgDelete");

                //if (Common.ExistsContentTemplateForMessageContent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true || Common.ExistsContentTemplateForMessageContentSent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true || lbtnDelete.CommandArgument == ApplicationHelper.ContentemplateIdForPorfolioSMS || lbtnDelete.CommandArgument == ApplicationHelper.ContentemplateId)
                //{
                //    lbtnDelete.Enabled = false;
                //}
                //else
                //{
                    lbtnDelete.Enabled = true;
                    lbtnDelete.Attributes["onClick"] = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
                //}
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message);
                //this.latestException = ex;
            }
        }
    }
    protected string GetNameServiceType(object serviceTypeId)
    {
        string s;
        s = ServiceTypeService.GetServiceType(Convert.ToInt32(serviceTypeId)).ServiceTypeDescription;
        return s;
    }
    

}

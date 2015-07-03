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

using CoreSecurityServiceBusiness = CoreSecurityService.Business;
using CoreSecurityServiceEntities = CoreSecurityService.Entities;
using System.Collections.Generic;



public partial class CustomerAccount : System.Web.UI.Page
{
    protected CustomerColumns orderByCustomerAccount
    {
        get
        {
            if (Session["orderByCustomerAccount"] == null) Session["orderByCustomerAccount"] = CustomerColumns.CustomerId;
            return (CustomerColumns)Session["orderByCustomerAccount"];
        }
        set { Session["orderByCustomerAccount"] = value; }
    }
    protected string orderDirectionCustomerAccount
    {
        get
        {
            if (Session["orderDirectionCustomerAccount"] == null) Session["orderDirectionCustomerAccount"] = "ASC";
            return (string)Session["orderDirectionCustomerAccount"];
        }
        set { Session["orderDirectionCustomerAccount"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setTextForPageLoad();
        if (!IsPostBack)
        {
            this.GetParameters();            
        }
        this.UpdateInterface();
    }

    private void GetParameters()
    {
        orderByCustomerAccount = CustomerColumns.CustomerId;
        orderDirectionCustomerAccount = "ASC";
    }

    private void setTextForPageLoad()
    {
        this.ButtonSynchronize.Value = Resources.UIResource.CoreSynchronize;
        this.SearchInput.Text = Resources.UIResource.SearchButton;
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }

    private void UpdateInterface()
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = CustomerService.GetCustomerListSearch(1,
            this.InputSearchCustomerid.Value == string.Empty ? "All" : this.InputSearchCustomerid.Value,
            this.InputSearchCustomerName.Value == string.Empty ? "All" : this.InputSearchCustomerName.Value,
            this.InputSearchCustomerNameViet.Value == string.Empty ? "All" : this.InputSearchCustomerNameViet.Value,
            this.InputSearchEmail.Value == string.Empty ? "All" : this.InputSearchEmail.Value,
            InputTel.Value == string.Empty ? "All" : InputTel.Value,orderByCustomerAccount
           , orderDirectionCustomerAccount, this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterData.DataBind();
        this.topPaging.PageSize = this.bottomPaging.PageSize = ApplicationHelper.PageSize;
        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;
    }
    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }
    protected void InsertButton_Click(object ob, EventArgs e)
    {
        Response.Redirect("CustomerNoAccountDetail.aspx?action=new");
    }
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            CustomerService.DeleteCustomer(Convert.ToString(e.CommandArgument));
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

                //if (Common.ExistServiceTypeIdForMessageContent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true)
                //{
                //    lbtnDelete.Enabled = false;
                //}
                //else
                //{
                //lbtnDelete.Enabled = false;
                //lbtnDelete.Attributes"onClick" = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
                //}
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message);
                //this.latestException = ex;
            }
        }
    }
    protected string GetNameContentTemplate(object contentTemplateId)
    {
        string s;
        s = ContentTemplateService.GetContentTemplate(Convert.ToInt32(contentTemplateId)).Description;
        return s;
    }
    protected string ConverFormat(object dob)
    {
        string s;
        if (ApplicationHelper.ConvertDateToString(dob) == "01/01/1900")
            s = "";
        else
            s = ApplicationHelper.ConvertDateToString(dob);
        return s;
    }

    protected void ButtonDeleteSelect_Click(object ob, EventArgs e)
    {
        //string selectedItems = Request.Form"CheckBoxDelete";
        //Int32 messageContentSentId;
        //if (selectedItems == string.Empty || selectedItems == null) return;
        //foreach (string selectedItemId in selectedItems.Split(','))
        //{
        //    messageContentSentId = Convert.ToInt32(selectedItemId);
        //    MessageContentSentService.DeleteMessageContentSent(messageContentSentId);
        //}
        //this.UpdateInterface();
    }

    protected void SearchInput_Click(object ob, EventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = 1;
        this.UpdateInterface();
    }

    protected async void ButtonSynchronize_Click(object sender, EventArgs e)
    {
        //string[] data = new string[0];        
        //int numberCustomerSent = 0;     
        // lấy danh sách khách hàng từ core
        var customerCollectionCore = new List<DAOvEntitiesFramwork.Customer>();
        customerCollectionCore = await DAOvEntitiesFramwork.Customer_Goline.GetListCustomer_Goline();
        //


        // khi có khách hàng mới mở tài khoản gửi mail và SMS
        //ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(Convert.ToInt32(ApplicationHelper.ContentemplateId));
        foreach (DAOvEntitiesFramwork.Customer customer in customerCollectionCore)
        {
            Customer customervfs = null;
            Customer customerCheck = CustomerService.GetCustomer(customer.CustomerId);
            if (customerCheck == null)
            {
                customervfs = new Customer();
            }
            else
            {
                customervfs = customerCheck;
            }
            
            customervfs = this.getSynchronize(customervfs, customer);

            //if (customerCheck == null) // khách mới tạo mới KH
            //{
            //    CustomerService.CreateCustomer(customervfs);
            //    //ImportService.CreateMessageWhenNewCustomer(customervfs, contentTemplate, "M");
            //}
            //else// khách cũ thì update lại thông tin Email, mobile ...
            //{
            //    CustomerService.UpdateCustomer(customervfs);
            //}
        }
        spSynclickWarning.InnerHtml = "Đồng bộ thành công";
        this.UpdateInterface();
    }

    private Customer getSynchronize(Customer customervfs, DAOvEntitiesFramwork.Customer customer)
    {
        customervfs.TypeID = 1;
        customervfs.BranchCode = customer.BranchCode.ToString();
        customervfs.CustomerId = customer.CustomerId;
        customervfs.BrokerId = 1;
        customervfs.CardType = 1;
        customervfs.Email = customer.Email;
        customervfs.Mobile = customer.Mobile;
        customervfs.Dob = customer.Birthday;
        customervfs.Notes = customer.Notes;
        customervfs.CustomerNameViet = customer.Name;
        customervfs.CustomerName = customer.Name;
        customervfs.Address = customer.Address;


        return customervfs;
    }
    
    protected void GetValue()
    {
        orderDirectionCustomerAccount = orderDirectionCustomerAccount == "ASC" ? "DESC" : "ASC";
    }

    protected void SortCustomerId(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.CustomerId;
        this.UpdateInterface();
    }
    protected void SortCustomerName(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.CustomerNameViet;
        this.UpdateInterface();        
    }

    protected void SortDOB(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Dob;
        this.UpdateInterface();        
    }

    protected void SortMobile(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Mobile;
        this.UpdateInterface();        
    }

    protected void SortEmail(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Email;
        this.UpdateInterface();        
    }
    protected object GetOrderDirectionIndicator(string property)
    {
        if (property.Equals(orderByCustomerAccount.ToString()))
        {
            return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectionCustomerAccount);
        }
        else
            return "";
    }
    

}

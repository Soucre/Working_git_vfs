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

public partial class CustomerNoAccountDetail : System.Web.UI.Page
{
    string customerid;
    string action;
    Customer customer;
    //CoreSecurityServiceEntity.CoreSecurityService.Customer 
    protected void Page_Load(object sender, EventArgs e)
    {
        this.paramater();
        this.setTextForPageLoad();
        if (!IsPostBack)
        {
            this.LoadButton();
            if (this.action == "modify") this.LoadModify();
        }        
    }
    private void LoadButton()
    {
        if (this.action == "new")
        {
            this.ButtonDelete.Visible = false;
        }
        else
        {
            this.InputCustomerId.Disabled = true;
            this.ButtonDelete.Visible = true;
        }
    }
    private void LoadModify()
    {
        customer = CustomerService.GetCustomer(customerid);
        if (customer != null)
        {
            this.InputCustomerId.Value = customer.CustomerId;
            this.InputCustomerName.Value = customer.CustomerName;
            this.InputCustomerNameViet.Value = customer.CustomerNameViet;
            this.SelectDomesticForeign.Value = customer.DomesticForeign;
            if (ApplicationHelper.ConvertDateToString(customer.Dob) == "01/01/1900")
            {
                this.Selectdob.Text = "";
            }
            else
            {
                this.Selectdob.Text = ApplicationHelper.ConvertDateToString(customer.Dob);
            }
            this.SelectSex.Value = customer.Sex;
            if (ApplicationHelper.ConvertDateToString(customer.OpenDate) == "01/01/1900")
            {
                this.SelectOpendate.Text = "";
            }
            else
            {
                this.SelectOpendate.Text = ApplicationHelper.ConvertDateToString(customer.OpenDate);
            }
            this.InputCardIdentityDetail.Value = customer.CardIdentity;
            this.InputAddress.Value = customer.AddressViet;
            this.inputTel.Value = customer.Tel;
            this.InputFax.Value = customer.Fax;
            this.InputMobile1.Value = customer.Mobile;
            this.InputModile2.Value = customer.Mobile2;
            this.inputEmail.Value = customer.Email;
        }
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);        
        if (this.action == "modify")
        {         
            this.customerid = AppConstants.GetString(AppConstants.QS_CUSTOMER_ID);
        }   
    }
    private void setTextForPageLoad()
    {
        this.ButtonDelete.Value = Resources.UIResource.Delete;
        this.ButtonCallBack.Value = Resources.UIResource.Cancel;

        if (this.action == "modify")
        {
            this.header1.InnerText = this.Page.Title = Resources.UIResource.updateCustomer;            
        }
        else
        {
            this.header1.InnerText = this.Page.Title = Resources.UIResource.AddCustomer;            
        }
        
    }
    protected void buttonSave_ServerClick(object sender, EventArgs e)
    {
        this.SaveDetail();
    }

    private void SaveDetail()
    {
        this.paramater();
        #region ThongBaoLoi
        this.ErrorInfo.InnerText = "";
        this.labelExistsCustomer.InnerText = "";
        this.ErrorInfoCustomerName.InnerText = "";
        if (this.InputCustomerId.Value == string.Empty)
        {
            this.ErrorInfo.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }       
        if (this.InputCustomerNameViet.Value == string.Empty)
        {
            this.ErrorInfoName.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }      
        #endregion
        if (this.action == "modify")
        {
            this.customer = CustomerService.GetCustomer(customerid);
        }
        else
        {
            this.customer = new Customer();
        }
        if (this.customer != null)
        {            
            customer.CustomerId = this.InputCustomerId.Value;
            customer.CustomerName = this.InputCustomerName.Value;
            customer.CustomerNameViet = this.InputCustomerNameViet.Value;
            customer.DomesticForeign = this.SelectDomesticForeign.Value;
            if (this.Selectdob.Text == "")
            {
                this.customer.Dob = ApplicationHelper.ConvertStringToDate("01/01/1900");
            }
            else
            {
                customer.Dob = ApplicationHelper.ConvertStringToDate(this.Selectdob.Text);
            }
            customer.Sex = this.SelectSex.Value;
            if (this.SelectOpendate.Text == "")
            {
                customer.OpenDate = ApplicationHelper.ConvertStringToDate("01/01/1900");
            }
            else
            {
                customer.OpenDate = ApplicationHelper.ConvertStringToDate(SelectOpendate.Text);
            }
            customer.CardIdentity = this.InputCardIdentityDetail.Value;
            customer.AddressViet = this.InputAddress.Value;
            customer.Tel = this.inputTel.Value;
            customer.Fax = this.InputFax.Value;
            customer.Mobile = this.InputMobile1.Value;
            customer.Mobile2 = this.InputModile2.Value;
            customer.Email = this.inputEmail.Value;
            customer.BranchCode = "100";
            customer.BrokerId = 0;
            customer.TypeID = 2;
        }        
        if (this.action == "modify")
        {
            CustomerService.UpdateCustomer(customer);
        }
        else
        {
            if (CustomerService.GetCustomer(customer.CustomerId) != null)
            {
                this.labelExistsCustomer.InnerText = Resources.UIResource.ExistsCustomerId;
                return;
            } 
            CustomerService.CreateCustomer(customer);
        }
        Response.Redirect("CustomerNoAccount.aspx");
    }
    protected void ButtonDelete_onserverclick(object sender, EventArgs e)
    {
        this.paramater();
        customer = CustomerService.GetCustomer(customerid);
        if (customer != null)
        {
            CustomerService.DeleteCustomer(customerid);
        }
        Response.Redirect("CustomerNoAccount.aspx");
    }
    protected void ButtonCallBack_onserverclick(object sender, EventArgs e)
    {
        Response.Redirect("CustomerNoAccount.aspx");
    }
    
}

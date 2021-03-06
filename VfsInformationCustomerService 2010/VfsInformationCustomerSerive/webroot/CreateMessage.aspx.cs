using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;
using Vfs.WebCrawler.Utility;


public partial class CreateMessage : System.Web.UI.Page
{
    protected CustomerColumns orderBytab2
    {
        get
        {
            if (Session["orderBytab2"] == null) Session["orderBytab2"] = CustomerColumns.CustomerId;
            return (CustomerColumns)Session["orderBytab2"];
        }
        set { Session["orderBytab2"] = value; }
    }
    protected string orderDirectiontab2
    {
        get
        {
            if (Session["orderDirectiontab2"] == null) Session["orderDirectiontab2"] = "ASC";
            return (string)Session["orderDirectiontab2"];
        }
        set { Session["orderDirectiontab2"] = value; }
    }
    protected CustomerColumns orderBytab1
    {
        get
        {
            if (Session["orderBytab1"] == null) Session["orderBytab1"] = CustomerColumns.CustomerId;
            return (CustomerColumns)Session["orderBytab1"];
        }
        set { Session["orderBytab1"] = value; }
    }
    protected string orderDirectiontab1
    {
        get
        {
            if (Session["orderDirectiontab1"] == null) Session["orderDirectiontab1"] = "ASC";
            return (string)Session["orderDirectiontab1"];
        }
        set { Session["orderDirectiontab1"] = value; }
    }

    public int totalCustomerTab1
    {
        get { return (int)Session["totalCustomerTab1"]; }
        set { Session["totalCustomerTab1"] = value; }
    }


    public int totalCustomerTab2
    {
        get { return (int)Session["totalCustomerTab2"]; }
        set { Session["totalCustomerTab2"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadInfo();
        if (!IsPostBack)
        {
            GetParameters();
            UpdateInterfaceTab1();    
        }
    }

    private void LoadInfo()
    {
        this.SendMessengerButton.Value = Resources.UIResource.create;
        this.infoError.InnerText = "";
        this.infoTab2Label.InnerText = "";
        this.infoTab1Label.InnerText = "";
    }

    private void GetParameters()
    {
        orderBytab2 = CustomerColumns.CustomerId;
        orderDirectiontab2 = "ASC";
        orderBytab1 = CustomerColumns.CustomerId;
        orderDirectiontab1 = "ASC";
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.LoadTemplates();
        this.SettingInterface();
    }

    protected string getTotalCustomer()
    {
        if (MultiviewCustomer.ActiveViewIndex == 0)
        {
            return Resources.UIResource.Find + totalCustomerTab1.ToString() + Resources.UIResource.customer;
        }
        else
        {
            return Resources.UIResource.Find + totalCustomerTab2.ToString() + Resources.UIResource.customer;
        } 
    }
       
    
    private void LoadTemplates()
    {
        this.templateDropDownList.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.ModifiedDate, "DESC");
        this.templateDropDownList.DataTextField = "Description";
        this.templateDropDownList.DataValueField = "ContentTemplateID";
        this.templateDropDownList.DataBind();
    }

    protected void SendMessengerButton_onserverclick(object sender, EventArgs e)
    {
        string items = string.Empty;
        int numberCustomerSent = 0;
        if (MultiviewCustomer.ActiveViewIndex == 0)
        {
            items = Request.Form["selectedItem"];            
        }
        else
        {
            items = Request.Form["CheckBoxSendTab2"];
        }       

        int i=0;
        if (items == null)
        {
            #region infosucess
            if (MultiviewCustomer.ActiveViewIndex == 0)
            {
                this.infoTab1Label.InnerText = Resources.UIResource.NotYetCheckCustomer;
                this.infoTab1Label.Attributes["class"] = "inforError";                
            }
            else
            {
                this.infoTab2Label.InnerText = Resources.UIResource.NotYetCheckCustomer;
                this.infoTab2Label.Attributes["class"] = "inforError";
            }
            #endregion
            return;
        }
        string[] data = new string[LengthofArray(items)];        
        ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString()));
        if (contentTemplate.ServiceTypeID == 1)
        {//Email
            foreach (string item in items.Split(','))
            {
                Customer customer = CustomerService.GetCustomer(item);
                if (customer.SendYN == "Y")
                {                                      
                    data[i] = customer.Email;
                    i++;
                }
            }
        }
        else
        {//SMS
            foreach (string item in items.Split(','))
            {
                    data[i] = CustomerService.GetCustomer(item).Mobile;
                    i++;
            }
        }        
        //get data
        if (contentTemplate.ContentTemplateID == Convert.ToInt32(ApplicationHelper.DailyContentTemplateId) || contentTemplate.ContentTemplateID == Convert.ToInt32(ApplicationHelper.RelatedContentTemplateId))
        {
            ImportService.CreateMessageWithEmailEncrypt(data, ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString())), ref numberCustomerSent);
        }
        else
        {
            ImportService.CreateMessageWithEmail(data, ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString())), ref numberCustomerSent);
        }
        #region infosucess
        if (MultiviewCustomer.ActiveViewIndex == 0)
        {
            this.infoTab1Label.InnerText = Resources.UIResource.CreateMessengerSucess + " " + numberCustomerSent.ToString() + Resources.UIResource.customer;            
            this.infoTab1Label.Attributes["class"] = "inforSeccessful";            
        }
        else
        {
            this.infoTab2Label.InnerText = Resources.UIResource.CreateMessengerSucess + " " + numberCustomerSent.ToString() + Resources.UIResource.customer;
            this.infoTab2Label.Attributes["class"] = "inforSeccessful" ;
        }
        #endregion
    }

    protected int LengthofArray(string items)
    {
        int i = 0;
        foreach (string item in items.Split(','))
        {            
            i++;
        }
        return i;
    }
    protected void Button1_onserverclick(object sender, EventArgs e)
    {
        if (Page.IsValid == false) return;

        try
        {
            ImportService.CreateAccountForStox(this.uploadFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.UploadFolderPath), this.uploadFile.FileName, ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString())));
            //successMessage.Text = Resources.UIResource.ImportUpdateSuccessMessage;
            this.infoError.InnerText = Resources.UIResource.CreateMessengerSucess;
            this.infoError.Attributes["class"] = "inforSeccessful";
        }
        catch (Exception ex)
        {
            log4net.Util.LogLog.Error(ex.Message, ex);
            //successMessage.Text = ex.Message;
        }
    }
   
    protected void importCreateMessageButton_Click(object sender, EventArgs e)
    {
        //this.SettingInterface();
        if (Page.IsValid == false) return;
        this.infoError.InnerText = "";
        
        if (uploadFile.FileName == string.Empty)
        {
            this.infoError.Attributes["class"] = "inforError";
            this.infoError.InnerText = Resources.UIResource.UploadFileErrorMessage;        
            return;
        }
        
        try
        {
            ImportService.CreateMessageWithEmail(this.uploadFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.UploadFolderPath), this.uploadFile.FileName, ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString())));
            //successMessage.Text = Resources.UIResource.ImportUpdateSuccessMessage;
            this.infoError.InnerText = Resources.UIResource.CreateMessengerSucess;
            this.infoError.Attributes["class"] = "inforSeccessful";
        }
        catch (Exception ex)
        {
            log4net.Util.LogLog.Error(ex.Message, ex);
            //successMessage.Text = ex.Message;
        }
    }

    private void UpdateInterfaceTab1()
    {
        int totalRow = 0;
        this.RepeaterDataTab1.DataSource = CustomerService.GetCustomerListSearch(1,
            this.InputSearchCustomerid.Value == string.Empty ? "All" : this.InputSearchCustomerid.Value,
            this.InputSearchCustomerName.Value == string.Empty ? "All" : this.InputSearchCustomerName.Value,
            this.InputSearchCustomerNameViet.Value == string.Empty ? "All" : this.InputSearchCustomerNameViet.Value,
            this.InputSearchEmail.Value == string.Empty ? "All" : this.InputSearchEmail.Value,
            InputTel.Value == string.Empty ? "All" : InputTel.Value,
           orderBytab1, orderDirectiontab1,0 , 0 , out totalRow);
        totalCustomerTab1 = totalRow;
        this.RepeaterDataTab1.DataBind();     
    }

    private void UpdateInterfaceTab2()
    {
        Int32 totalRow;
        this.RepeaterDataTab2.DataSource = CustomerService.GetCustomerListSearch(2,
            this.InputSearchCustomeridTab2.Value == string.Empty ? "All" : this.InputSearchCustomeridTab2.Value,
            this.InputSearchCustomerNameTab2.Value == string.Empty ? "All" : this.InputSearchCustomerNameTab2.Value,
            this.InputSearchCustomerNameVietTab2.Value == string.Empty ? "All" : this.InputSearchCustomerNameVietTab2.Value,
            this.InputSearchEmailTab2.Value == string.Empty ? "All" : this.InputSearchEmailTab2.Value,
            InputTelTab2.Value == string.Empty ? "All" : InputTelTab2.Value,
           orderBytab2, orderDirectiontab2, 0, 0, out totalRow);
        totalCustomerTab2 = totalRow;
        this.RepeaterDataTab2.DataBind();        
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

    private void SettingInterface()
    {
        //this.importCreateMessageButton.Value = Resources.UIResource.create;
        //this.uploadFileRequiredFieldValidator.ErrorMessage = Resources.UIResource.UploadFileErrorMessage;        
    }
    
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiviewCustomer.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
        if (e.Item.Value == "0")
        {
            this.UpdateInterfaceTab1();
            Menu1.Items[0].Enabled = false;
            Menu1.Items[1].Enabled = true;
        }
        else
        {
            this.UpdateInterfaceTab2();
            Menu1.Items[0].Enabled = true;
            Menu1.Items[1].Enabled = false;
        }
        //for (int i = 0; i < Menu1.Items.Count; i++)
        //{
        //    if (i == Convert.ToInt32(e.Item.Value))
        //        Menu1.Items[i].ImageUrl = "~/_assets/img/selectedtab.GIF";
        //    else
        //        Menu1.Items[i].ImageUrl = "~/_assets/img/unselectedtab.GIF";
        //}
    }

    protected void SearchInputTab1_Click(object ob, EventArgs e)
    {
        this.UpdateInterfaceTab1();
    }

    protected void SearchInputTab2_Click(object ob, EventArgs e)
    {
        this.UpdateInterfaceTab2();        
    }

    protected void GetValueTab2()
    {
        orderDirectiontab2 = orderDirectiontab2 == "ASC" ? "DESC" : "ASC";
    }

    protected void SortCustomerIdListtab2(object ob, EventArgs e)
    {
        GetValueTab2();
        orderBytab2 = CustomerColumns.CustomerId;        
        this.UpdateInterfaceTab2();        
    }

    protected void SortCustomerNameTab2(object ob, EventArgs e)
    {
        GetValueTab2();
        orderBytab2 = CustomerColumns.CustomerNameViet;
        this.UpdateInterfaceTab2();        
    }

    protected void SortDOBTab2(object ob, EventArgs e)
    {
        GetValueTab2();
        orderBytab2 = CustomerColumns.Dob;
        this.UpdateInterfaceTab2();        
    }

    protected void SortMobileTab2(object ob, EventArgs e)
    {
        GetValueTab2();
        orderBytab2 = CustomerColumns.Mobile;
        this.UpdateInterfaceTab2();        
    }

    protected void SortEmailTab2(object ob, EventArgs e)
    {
        GetValueTab2();
        orderBytab2 = CustomerColumns.Email;
        this.UpdateInterfaceTab2();        
    }

    protected void GetValueTab1()
    {
        orderDirectiontab1 = orderDirectiontab1 == "ASC" ? "DESC" : "ASC";
    }

    protected void SortCustomerIdListtab1(object ob, EventArgs e)
    {
        GetValueTab1();
        orderBytab1 = CustomerColumns.CustomerId;
        this.UpdateInterfaceTab1();
    }

    protected void SortCustomerNameTab1(object ob, EventArgs e)
    {
        GetValueTab1();
        orderBytab1 = CustomerColumns.CustomerNameViet;
        this.UpdateInterfaceTab1();
    }

    protected void SortDOBTab1(object ob, EventArgs e)
    {
        GetValueTab1();
        orderBytab1 = CustomerColumns.Dob;
        this.UpdateInterfaceTab1();
    }

    protected void SortMobileTab1(object ob, EventArgs e)
    {
        GetValueTab1();
        orderBytab1 = CustomerColumns.Mobile;
        this.UpdateInterfaceTab1();
    }

    protected void SortEmailTab1(object ob, EventArgs e)
    {
        GetValueTab1();
        orderBytab1 = CustomerColumns.Email;
        this.UpdateInterfaceTab1();
    }

    protected object GetOrderDirectionIndicatorTab2(string property)
    {
        if (property.Equals(orderBytab2.ToString()))
        {
            return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectiontab2);
        }
        else
        {            
            return "";
        }        
    }

    protected object GetOrderDirectionIndicatorTab1(string property)
    {
        if (property.Equals(orderBytab1.ToString()))
        {
            return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectiontab1);
        }
        else
            return "";
    }
}
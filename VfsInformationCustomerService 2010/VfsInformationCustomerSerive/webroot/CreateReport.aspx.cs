using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAOvEntitiesFramwork_CusServices;
using VfsCustomerService.Business;

using Vfs.WebCrawler.Utility;
public partial class CreateReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadDefaultValue();
        if (!Page.IsPostBack)
        {
            LoadDefaultValueNonPostBack();
        }
    }

    private void LoadDefaultValue()
    {
        // get messenge SMS
        //if (QuestionCheck.Checked)
        //{
        //    editSMSDialog.Style.Add("display", "block");
        //}
        //else
        //{
        //    editSMSDialog.Style.Add("display", "none");
        //}
    }

    private void LoadDefaultValueNonPostBack()
    {
        
        VfsCustomerService.Entities.ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(ApplicationHelper.ReportContentTemplate);
        InputBodyMessager.Value = contentTemplate.BodyMessage;
    }

    protected override void OnInit(EventArgs e)
    {        
        rbtLstTimeView.SelectedValue = "8";
    }
    /// <summary>
    /// Tạo báo cáo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ReportDAO reDAO = new ReportDAO();
    protected void CreateReport_Click(object sender, EventArgs e)
    {
        
        if (Page.IsValid) // nếu validation ok
        {
            #region kiem tra dinh dang file upload
            if (!CheckPDFFileType(SourceFile.PostedFile.FileName)) // neu khong la file pdf
            {
                uploadFileRequiredFieldValidator.ErrorMessage = "chọn file pdf";                
                return;
            }
            #endregion
            
            #region upload file pdf
            int filesize = 0;
            string fileNameUpload = ApplicationHelper.UploadPDF(SourceFile, ApplicationHelper.UploadFolderReport, ".pdf", out filesize);
            #endregion

            #region luu báo cáo vào database
            Report rp = new Report();
            rp.CreateDate = DateTime.Now;
            rp.DateViewCustomer = DateTime.Now.AddHours(int.Parse(rbtLstTimeView.SelectedValue));
            rp.IdReportType = int.Parse(reportTypeDropDownList.SelectedValue);
            rp.Ticker = StockCodeInput.Text;
            rp.Title = TitleReport.Text;
            rp.UploadDir = fileNameUpload;
            rp.FileSize = filesize/1024;
            reDAO.InsertReport(rp); // insert to database
            if (QuestionCheck.Checked) // gửi tin nhắn cho khách hành VIP
            {
                SendSMSForVIPType();
            }
            #endregion
        }
    }

    private void SendSMSForVIPType()
    {
        List<CustomerCustom> listCustomerVIP = new List<CustomerCustom>();
        listCustomerVIP = CustomerDAO.getListCustomerVIPType().Result;


    }

    /// <summary>
    /// Ajax từ client kiem tra noi dung tin nhan gui di
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static string SendSMSTest(string mobile)
    {
        System.Threading.Thread.Sleep(2000);

        return "Y";
    }

    /// <summary>
    /// Luu noi dung tin nhan tu client
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static string SaveTemplateAjax(string content)
    {
        System.Threading.Thread.Sleep(2000);
        //get template
        VfsCustomerService.Entities.ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(ApplicationHelper.ReportContentTemplate);
        contentTemplate.BodyMessage = VfsCustomerService.Utility.HtmlRemoval.StripTagsRegexCompiled(content);
        contentTemplate.ModifiedDate = DateTime.Now;
        try // update template
        {
            ContentTemplateService.UpdateContentTemplate(contentTemplate);
            return "Y";
        }
        catch (Exception)
        {
            return "N";
        }
        
    }
    protected bool CheckPDFFileType(string UploadFileName)
    {
        if (UploadFileName == "")
        {
            // There is no file selected 
            return false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

            if (Extension == "pdf")
            {
                return true; // Valid file type
            }
            else
            {
                return false; // Not valid file type
            }
        }
    }
    
}
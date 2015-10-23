using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAOvEntitiesFramwork_CusServices;

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
        this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
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

            #endregion


            Report rp = new Report();
            rp.CreateDate = DateTime.Now;
            rp.DateViewCustomer = DateTime.Now.AddHours(int.Parse(rbtLstTimeView.SelectedValue));
            rp.IdReportType = int.Parse(reportTypeDropDownList.SelectedValue);
            rp.Ticker = StockCodeInput.Text;
            rp.Title = TitleReport.Text;

            reDAO.InsertReport(rp); // insert to database
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
    protected void UploadFile(FileUpload fupload)
    {

    }
}
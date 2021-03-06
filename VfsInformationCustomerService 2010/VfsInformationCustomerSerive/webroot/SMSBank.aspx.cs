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
using VfsCustomerService.Entities;
using VfsCustomerService.Business;
using Vfs.WebCrawler.Utility;

public partial class SMSBank : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        statusLabel.InnerText = "";
    }
    protected void Send_Click(object sender, EventArgs e)
    {
        try
        {
            MessageContent messageContent = new MessageContent();
            if (String.IsNullOrEmpty(receiverTextBox.Text) || String.IsNullOrEmpty(ContentTextBox.Text))
            {
                statusLabel.InnerText = "Vui lòng điền đầy đủ thông tin";
                return;
            }
            messageContent.BodyContentType = "PlainText";
            messageContent.BodyEncoding = "UTF-8";
            messageContent.BodyMessage = ContentTextBox.Text.Trim();
            messageContent.Receiver = receiverTextBox.Text.Trim();
            messageContent.ServiceTypeID = 2;

            SendSMS sendSMS = new SendSMS();
            sendSMS.UserName = ApplicationHelper.SmsUserName;
            sendSMS.Password = ApplicationHelper.SmsPassword;
            sendSMS.Send(messageContent);

            statusLabel.InnerText = "Gửi thành công";
        }
        catch (Exception ex)
        {
            statusLabel.InnerText = "Không thể kết nối tới server SMS";
            
        }
        
    }
}

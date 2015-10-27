<%@ Page Title="Tạo báo cáo phân tích" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateReport.aspx.cs" Inherits="CreateReport" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">  
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>  

<h1>Tạo báo cáo</h1>     

<div>
    <input type="button" id="ButtonCreateReport" runat="server" value="Tạo báo cáo" onserverclick="CreateReport_Click" />
    <span class="warning_Messenge" runat="server" id="spSynclickWarning"></span>
    <table>     
         
        <tr>
            <td style="width:240px;"><%=Resources.UIResource.ReportTitle%></td>
            <td>
                <%--<input Id="TitleReport" name="TitleReport" maxlength="512" style="width:400px" />--%>                
                <asp:TextBox runat="server" ID="TitleReport"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TitleReportRequiredFieldValidator" runat="server" ControlToValidate="TitleReport"
                        ErrorMessage="Required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><%= Resources.UIResource.SelectFile%></td>
            <td>
                <asp:FileUpload id="SourceFile" runat="server" accept="application/pdf" />
                <asp:RequiredFieldValidator ID="uploadFileRequiredFieldValidator" runat="server" ControlToValidate="SourceFile"
                ErrorMessage="Required!"></asp:RequiredFieldValidator>     
    
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.SelectTimeView%></td>
            <td>
               <asp:RadioButtonList ID="rbtLstTimeView" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                <asp:ListItem Text="24" Value="24"></asp:ListItem>
            </asp:RadioButtonList>       
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.ReportType%></td>
            <td>
                <asp:DropDownList runat="server" ID="reportTypeDropDownList" >
                    <asp:ListItem Text="Chọn" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Cảm nhận thị trường" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Phân tích cổ phiếu" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Phân tích kỹ thuật" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Báo cáo chiến lược" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Phím hàng" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Snapshot" Value="6"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="reportTypeDropDownList"
                ErrorMessage="Required!" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
               
        </tr>
        <tr>
            <td>
                <%=Resources.UIResource.StockCode%>
            </td>
            <td>
                <%--<input name="StockCodeInput" maxlength="10" style="width:80px" />--%>
                <asp:TextBox runat="server" ID="StockCodeInput"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="StockCodeInput"
                        ErrorMessage="Required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td><%=Resources.UIResource.QuestionSMS%></td>
            <td>
                <input onclick="if ($(this).is(':checked')) { $('#ctl00_ContentPlaceHolder3_editSMSDialog').show(); } else { $('#ctl00_ContentPlaceHolder3_editSMSDialog').hide(); }" type="checkbox" id="QuestionCheck" name="QuestionCheck" runat="server" checked="checked" />
                <span id="editSMSDialog" runat="server"><a href="javascript:void(0)" id="BoxMessengContent">Nội dung tin nhắn</a></span>
            </td>
        </tr>
     <%--   <tr id="editSMSDialog" runat="server">
            <td><%=Resources.UIResource.MessengReportVIP%></td>
            <td>
                
            </td>
        </tr>--%>
    </table>
</div>
<div id="dialog" title="Cập nhật nội dung tin nhắn" style="display:none">
    <textarea runat="server" ID="InputBodyMessager" class="InputBodyMessager"  style="width: 90%; height:80%">
    </textarea>
    <%--<input type="hidden" id="hiddenInputBodyMessager" value="<%=InputBodyMessager.Value%>" />--%>

    <br />
    <input type="text" name="name" id="CheckSendSMS" placeholder="Số ĐT test nội dung SMS" maxlength="11"/>
    <span id="loading-image"></span>

</div>

 <script type="text/javascript" src="Js/CreateReport.js"></script>

  <script type = "text/javascript">
     
    </script>
</asp:Content>



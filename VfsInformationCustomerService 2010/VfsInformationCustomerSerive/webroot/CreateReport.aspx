<%@ Page Title="Tạo báo cáo phân tích" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateReport.aspx.cs" Inherits="CreateReport" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<h1>Tạo báo cáo</h1>     

<div>
    <input type="button" id="ButtonCreateReport" runat="server" value="Tạo báo cáo"/>
    <span class="warning_Messenge" runat="server" id="spSynclickWarning"></span>
    <table >     
         
        <tr>
            <td style="width:210px;"><%=Resources.UIResource.ReportTitle%></td>
            <td>
                <input name="TitleReport" maxlength="512" style="width:400px" />
            </td>
        </tr>
        <tr>
            <td><%= Resources.UIResource.SelectFile%></td>
            <td>
                <asp:FileUpload id="SourceFile" runat="server" />
                <asp:RequiredFieldValidator ID="uploadFileRequiredFieldValidator" runat="server" ControlToValidate="SourceFile"
                ErrorMessage="" EnableClientScript="false"></asp:RequiredFieldValidator>     
    
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
                <asp:DropDownList runat="server" ID="reportTypeDropDownList" ></asp:DropDownList>
            </td>
        </tr>
    </table>
</div>

</asp:Content>



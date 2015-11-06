<%@ Page Title="Danh mục báo cáo" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportList.aspx.cs" Inherits="ReportList" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Danh mục báo cáo</h1>            
    <div class="createmesseng_div">
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" runat="server">
    </cc1:Pager>    
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand" OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">         
                                    <td style="width:350px"><a id="A1" href="#" ><b>Tiêu đề</b></a><a><%#GetOrderDirectionIndicator("CustomerId")%></a></td>                                    
                                    <td style="width:70px"><a id="A2" href="#"><b>Mã cổ phiếu</b></a><a><%#GetOrderDirectionIndicator("CustomerNameViet")%></a></td>
                                    <td style="width:150px"><a id="A3"  href="#"><b>Ngày tạo</b></a><a><%#GetOrderDirectionIndicator("Dob")%></a></td>                                   
                                    <td style="width:300px"><a id="A4"  href="#"><b>Tên tập tin</b></a><a><%#GetOrderDirectionIndicator("Mobile")%></a></td>                                   
                                    <td><a id="A5"  href="#"><b>Loại</b></a><a><%#GetOrderDirectionIndicator("Email")%></a></td>      
                                    <td><a id="A5"  href="#"><b>Download</b></a><a><%#GetOrderDirectionIndicator("Download")%></a></td>                                                                  
                                </tr>
                            </HeaderTemplate>   
                            <ItemTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                    <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("Title") %>"><%#Eval("Title")%></a></td>
                                    <td><%#Eval("Ticker")%> </a></td>
                                    <td><%#Eval("CreateDate")%></td>
                                    <td><%#Eval("UploadDir") %></td>
                                    <td><%#Eval("FileType")%></td>                                                                                                           
                                    <td><%#Eval("TotalDownload")%></td>        
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate> 
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                    <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("Title") %>"><%#Eval("Title")%></a></td>
                                    <td><%#Eval("Ticker")%> </a></td>
                                    <td><%#Eval("CreateDate")%></td>
                                    <td><%#Eval("UploadDir") %></td>
                                    <td><%#Eval("FileType")%></td>  
                                    <td><%#Eval("TotalDownload")%></td>                                                                                                                 
                                </tr>
                            </AlternatingItemTemplate>                    
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" id="bottomPaging" runat="server">
    </cc1:Pager>   
    </div>


</asp:Content>


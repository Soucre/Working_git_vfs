<%@ Page Title="Thống kê" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportCustomer.aspx.cs" Inherits="ReportCustomer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Thống kê khách hàng</h1>
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server">
                            <HeaderTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">         
                                    <td style="width:150px"><a id="A1" href="#" ><b>Mã khách hàng</b></a><a><%#GetOrderDirectionIndicator("CustomerId")%></a></td>                                    
                                    <td style="width:150px"><a id="A2" href="#"><b>Đăng nhập</b></a><a><%#GetOrderDirectionIndicator("CustomerNameViet")%></a></td>
                                    <td style="width:150px"><a id="A3"  href="#"><b>Download</b></a><a><%#GetOrderDirectionIndicator("Dob")%></a></td>                                   
                                    <td style="width:200px"><a id="A4"  href="#"><b>Ngày tạo</b></a><a><%#GetOrderDirectionIndicator("Mobile")%></a></td>                                                                       
                                </tr>
                            </HeaderTemplate>   
                            <ItemTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                    <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("CustomerId") %>"><%#Eval("CustomerId")%></a></td>
                                    <td><%#Eval("Total_Login")%> </td>
                                    <td><%#Eval("Total_Download")%></td>
                                    <td><%#Eval("CreateDate") %></td>                              
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate> 
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                    <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("CustomerId") %>"><%#Eval("CustomerId")%></a></td>
                                    <td><%#Eval("Total_Login")%></td>
                                    <td><%#Eval("Total_Download")%></td>
                                    <td><%#Eval("CreateDate") %></td>                              
                                </tr>
                            </AlternatingItemTemplate>                    
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
</asp:Content>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TienVaNoResult.ascx.cs" Inherits="VfsLookup.TienVaNoResult" %>
<div class="info_user">
            <div class="info_left">
            <table class="tbl_info_user">
                <tr class="even">
                    <td>
                    <asp:Label ID="lbTypeText" runat="server" Text="Tài khoản tổng hợp"></asp:Label> 
                    </td>
                    <td class="red_bold"> 
                        <asp:Label ID="lbTypeCode" runat="server" Text="N"></asp:Label> </td>
                </tr>
                 <tr class="odd">
                    <td>Chủ tài khoản</td>
                    <td class="bold">
                        <asp:Label ID="txtTKName" runat="server" Text="--------"></asp:Label>
                     </td>
                </tr>
                 <tr class="even">
                    <td>Số tài khoản</td>
                    <td class="bold">
                        <asp:Label ID="txtTkMa"  runat="server" Text="-"></asp:Label>
                        </td>
                </tr>
                 <tr class="odd">
                    <td>Ngày đầu kỳ</td>
                    <td><asp:Label ID="txtDateTn" runat="server" Text="-"></asp:Label></td>
                </tr>
                 <tr class="even">
                    <td>Ngày cuối kỳ</td>
                    <td><asp:Label ID="txtDateDn" runat="server" Text="-"></asp:Label></td>
                </tr>
            </table>
            </div>
            <div class="logo_right print_but_not_show">
                <img src="images/vietfirst.png" />
            </div>
        </div> <!-- info_user -->
        <div class="content_report">
            <div class="title_report">
                Báo cáo luân chuyển tiền trong kỳ </span>
            </div> <!-- title_report -->
            <div class="module_content_report">
                <table class="tbl_property table">
                     <tr class="<%=this.getClass(1) %>">
                        <td class="bold">1</td>
                        <td class="align_left bold">Tình trạng tiền đầu kỳ</td>
                        <td class="red_bold">
                        <asp:Label ID="lbTinhTrangDauKy" runat="server" Text="0"></asp:Label>
                        </td>
                        
                    </tr>
                    
                    <tr class="<%=this.getClass(1) %>">
                        <td class="bold">2</td>
                        <td class="align_left bold">Tiền nhận được trong kỳ</td>
                        <td class="red_bold"><asp:Label ID="lbTienNhanDuocTrongKy" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="tblTienNhan" runat="server">
                    <ItemTemplate  runat="server">
                        <tr class="<%=this.getClass(1) %>">
                            <td></td>
                            <td class="align_left"><%# Eval("ten") %></td>
                            <td><%# String.Format("{0:#,##0}", double.Parse(Eval("no").ToString()))%></td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                     
                    <tr class="<%=this.getClass(1) %>">
                        <td class="bold">3</td>
                        <td class="align_left bold">Tiền chi ra trong kỳ</td>
                        <td class="red_bold"><asp:Label ID="lbTienChiRaTrongKY" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="tblTienChi" runat="server">
                    <ItemTemplate  runat="server">
                        <tr class="<%=this.getClass(1) %>">
                            <td></td>
                            <td class="align_left"><%# Eval("ten") %></td>
                            <td><%# String.Format("{0:#,##0}", double.Parse(Eval("no").ToString()))%></td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <tr class="<%=this.getClass(1) %>">
                        <td class="bold">4</td>
                        <td class="align_left bold">Tình trạng tiền cuối kỳ</td>
                        <td class="red_bold">
                            <asp:Label ID="lbTinhTrangCuoiKy" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                     <tr class="<%=this.getClass(1) %>">
                        <td class="bold">5</td>
                        <td class="align_left bold">Tiền chờ về cuối kỳ</td>
                        <td class="red_bold">
                            <asp:Label ID="lbTienChoVeCuoiKy" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="tblTienCho"  runat="server">
                    <ItemTemplate  runat="server">
                        <tr class="<%=this.getClass(1) %>">
                            <td></td>
                            <td class="align_left"><%# Eval("ten") %></td>
                            <td><%# String.Format("{0:#,##0}", double.Parse(Eval("no").ToString()))%></td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                </table>
                
            </div> <!-- module_content_report -->
        </div> <!-- content_report -->
        <div class="print noPrint">
            <a href="#" onClick="window.print();return false">In báo cáo</a>
        </div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaiSanResult.ascx.cs" Inherits="VfsLookup.TaiSanResult" %>
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
                Báo cáo tài sản trong kỳ  <span></span>
            </div> <!-- title_report -->
            <div class="module_content_report">
                <table class="tbl_property table">
                    <tr class="even">
                        <td></td>
                        <td class="align_left"></td>
                        <td class="bold">Đầu kỳ</td>
                        <td class="bold">Cuối kỳ</td>
                    </tr>
                     <tr class="odd">
                        <td class="bold">1</td>
                        <td class="align_left bold">Tài sản</td>
                        <td class="red_bold">
                            <asp:Label ID="lbTaiSanTn" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="red_bold">
                            <asp:Label ID="lbTaiSanDn" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="tbTaiSan">
                    <ItemTemplate>
                     <tr class="<%=this.getClass(1) %>">
                        <td></td>
                        <td class="align_left"><%# Eval("ten")%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("dauky").ToString()))%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("cuoiky").ToString()))%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    
                     <tr class="odd">
                        <td class="bold">2</td>
                        <td class="align_left bold">Nợ</td>
                        <td class="red_bold">
                        <asp:Label ID="lbNoTn" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="red_bold">
                        <asp:Label ID="lbNoDn" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                     <asp:Repeater runat="server" ID="tbNo">
                    <ItemTemplate>
                     <tr class="<%=this.getClass(2) %>">
                        <td></td>
                        <td class="align_left"><%# Eval("ten") %></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("dauky").ToString())) %></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("cuoiky").ToString()))%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    
                    

                     <tr class="odd">
                        <td class="bold">3</td>
                        <td class="align_left bold">Tài sản ròng</td>
                        <td class="red_bold">
                        <asp:Label ID="lbTaiSanRongTn" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="red_bold">
                           <asp:Label ID="lbTaiSanRongDn" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                   
                </table>
                 <table class="tbl_property table">
                <tr class="odd">
                        <td class="bold">4</td>
                        <td class="align_left bold">Tài Sản Khác</td>
                        <td class="red_bold">
                        <asp:Label ID="lbTaiSanKhacTn" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="red_bold">
                        <asp:Label ID="lbTaiSanKhacDn" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                     <asp:Repeater runat="server" ID="tbTaiSanKhac">
                    <ItemTemplate>
                     <tr class="<%=this.getClass(3) %>">
                        <td></td>
                        <td class="align_left"><%# Eval("ten") %></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("dauky").ToString()))%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("cuoiky").ToString()))%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </table>
                <table class="tbl_result table">
                    <tr class="odd">
                        <td class="bold">
                            5
                        </td>
                        <td class="align_left bold">
                            Biến động tài sản từ đầu năm
                        </td>
                         <td class="bold">
                            Tài sản
                        </td>
                         <td class="bold">
                            Nợ
                        </td>
                         <td class="bold">
                            Tài sản ròng
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="tbReportYearly">
                    <ItemTemplate>
                    <tr class="<%=this.getClass(4) %>">
                        <td></td>
                        <td class="align_left"><%# Eval("ten")%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("taisan").ToString()))%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("no").ToString()))%></td>
                        <td><%# String.Format("{0:#,##0}", double.Parse(Eval("taisanrong").ToString()))%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                     
                </table> <!-- tbl_result -->
                <div class="chart">
                    <div class="title_chart">
                        Tài sản ròng
                    </div>
                    <canvas id="canvas" height="265" width="670"></canvas>
                </div>
             </div> <!-- module_content_report -->
        </div> <!-- content_report -->
        <div class="print noPrint" onClick="window.print();return false">
            <a href="#">In báo cáo</a>
        </div>
        <script>

            var data = {
                labels: [<%=this.getCharTitle() %>],
                datasets: [
                {
                    strokeColor: "#4f6228",
                    pointStrokeColor: "#fff",
                    data: [<%=this.getCharValue() %>]
                },
            ]
            }
            var myLine = new Chart(document.getElementById("canvas").getContext("2d")).Line(data);
        </script>
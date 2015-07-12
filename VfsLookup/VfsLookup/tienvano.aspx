<%@ Page Title="" Language="C#" MasterPageFile="~/VSIndex.Master" AutoEventWireup="true" CodeBehind="tienvano.aspx.cs" Inherits="VfsLookup.tienvano" %>
<%@ Register TagPrefix="uc" TagName="TienVaNoResult"   Src="~/TienVaNoResult.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title_page print_but_not_show">
<table width="100%"><tr>
<td style="text-align:left">Công ty cổ phần chứng khoán Nhất Việt</td>
<td style="text-align:right"><%=String.Format("{0:dd-MM-yyyy}",DateTime.Now) %></td>
</tr>
</table>
<div></div>
<h1 style="margin-top:30px;">BÁO CÁO LUÂN CHUYỂN TIỀN TRONG KỲ</h1>
</div>
<div class="content">
        <div class="search_ck noPrint">
            <form method="get">
                <div class="search_left">
                   <!--  <label>Chủ tài khoản:</label>
                    <input type="text" class="txt_gray"> -->
                    <label>Số tài khoản:</label>
                    <!--<input name="acccode" type="text" value="<=this.maTk %>">-->
                    
                      <select name="acccode">
                        <%=this.getAccOption(this.maTk)%>
                    </select>
                    
                    <label>Ngày đầu kỳ:</label>
                   <input type="text" name="txtDateTn" class="datepicker1" value="<%=Request["txtDateTn"] %>"/>
                    <!-- <span class="icon_calendar" ></span> -->
                     
                    <label>Ngày cuối kỳ:</label>
                    <input type="text" name="txtDateDn" class="datepicker2" value="<%=Request["txtDateDn"] %>"/>
                    <!-- <span class="icon_calendar" ></span> -->
                    <select name="type">
                        <option value="N" <%=Request["type"]=="N"?"selected='selected'":"" %>>Tách tài khoản</option>
                        <option value="Y" <%=Request["type"]=="Y"?"selected='selected'":"" %>>Tài khoản tổng hợp</option>
                    </select>
                     <input type="submit" value="Xem">
                     <div class="clear"></div>
                </div> <!-- search_left -->
                </form>
                
        </div> <!-- search_ck -->
        <form id="Form1" runat="server" method="get">
                 <asp:Label ID="txtMessageError" runat="server" Text="" ForeColor="Red"></asp:Label>
               <uc:TienVaNoResult runat="server" ID="ctr_tienvano_result" />
            </form>
    </div>
	<script>
	    $(".tienvano").addClass("active");
	    $(function () {
	        $(".datepicker1").datepicker({
	            showOn: "button",
	            buttonImage: "images/icon_calendar.png",
	            buttonImageOnly: true,
	            dateFormat: "dd/mm/yy",
	            maxDate: "0"
	        });
	    });
	    $(function () {
	        $(".datepicker2").datepicker({
	            showOn: "button",
	            buttonImage: "images/icon_calendar.png",
	            buttonImageOnly: true,
	            dateFormat: "dd/mm/yy",
	            maxDate: "0"
	        });
	    });


	    
    </script>
</asp:Content>

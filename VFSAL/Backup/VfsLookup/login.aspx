<%@ Page Title="" Language="C#" MasterPageFile="~/VSIndex.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="VfsLookup.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="login">
        <div class="title_login">
            <span class="notice"></span>
            <p>Vui lòng đăng nhập để xem thông tin</p>
        </div> <!-- title_login -->
        <div class="form_login" >
            <form method="post" runat='server' >
                <label class="lbl_text">Tên tài khoản:</label>
                <asp:TextBox class="txt_acc_name" ID="txt_username"  runat="server" TextMode="SingleLine"></asp:TextBox>
                <div class="clear"></div>
                <label class="lbl_text">Mật khẩu:</label>
                <asp:TextBox ID="txt_password"  runat="server" TextMode="Password"></asp:TextBox>
                <div class="clear"></div>
                <asp:Button ID="btnSubmitLogin"  runat="server" Text="Đăng nhập" 
                    onclick="btnSubmitLogin_Click" />
                <!--<input id="checkbox-1-1" class="checkbox" type="checkbox">
                <asp:CheckBox ID="cbCookies" runat="server"  />
                <label for="checkbox-1-1"></label>
                Ghi nhớ-->
            
            </form>
           <div class="module_registy">
                <ul>
                    <li><a target="_blank" href="http://vfs.com.vn/trangchu/tabid/56/ctl/Register/language/vi-VN/Default.aspx">Đăng ký tài khoản</a></li>
                    <li><a target="_blank" href="http://vfs.com.vn/trangchu/tabid/56/ctl/SendPassword/language/vi-VN/Default.aspx">Lấy lại mật khẩu</a></li>
                </ul>
           </div>
        </div> <!-- form_login -->
        
         <div class="module_Warning">
                    
                    <asp:Label ID="lbMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                
           </div>
        
    </div> <!-- login -->
    <script>
        $(".txt_acc_name").blur(function () {
            var text = "094C000000";
            if (this.value.length < 6 && this.value.length >0) {
                this.value = text.substr(0, text.length - this.value.length) + this.value;
            }
        });
    </script>
</asp:Content>

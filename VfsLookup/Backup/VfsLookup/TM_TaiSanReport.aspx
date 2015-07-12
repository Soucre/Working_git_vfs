<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_TaiSanReport.aspx.cs" Inherits="VfsLookup._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbName" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
    
        <asp:GridView ID="GridView3" runat="server">
        </asp:GridView>
    
        <br />
        <asp:Label ID="lbTSRongTN" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lbTSRongDN" runat="server" Text="Label"></asp:Label>
    
        <asp:GridView ID="GridView4" runat="server">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>

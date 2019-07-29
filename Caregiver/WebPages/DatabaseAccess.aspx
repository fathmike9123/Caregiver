<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseAccess.aspx.cs" Inherits="Caregiver.Web_Pages.DatabaseAccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Database Access (Admins Only)"></asp:Label>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Home Page</asp:LinkButton>
        </div>
    </form>
</body>
</html>

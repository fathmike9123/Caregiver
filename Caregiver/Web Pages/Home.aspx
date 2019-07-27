<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Caregiver.Web_Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Home Page"></asp:Label>
            <br />
            <br />
            <asp:LinkButton ID="lbViewAllPatients" runat="server" OnClick="lbViewAllPatients_Click">View All Patients</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lbCreateNew" runat="server" OnClick="lbCreateNew_Click">Create New Patient</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lbSearch" runat="server" OnClick="lbSearch_Click">Search for Patient</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lbDatabase" runat="server" OnClick="lbDatabase_Click">View Database (Admins Only)</asp:LinkButton>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lbSignOut" runat="server" OnClick="lbSignOut_Click">Sign Out</asp:LinkButton>
        </div>
    </form>
</body>
</html>

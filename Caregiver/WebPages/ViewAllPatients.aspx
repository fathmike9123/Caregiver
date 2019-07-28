<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllPatients.aspx.cs" Inherits="Caregiver.Web_Pages.ViewAllPatients" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="View All Patients"></asp:Label>
            <br />
            <br />
            (we have to somehow figure out how to dynamically add LinkButtons<br />
            based on the number of patients we have)<br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server">Patient 1</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton2" runat="server">Patient 2</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton3" runat="server">Patient 3</asp:LinkButton>
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lbReports" runat="server" OnClick="lbReports_Click">View Patient Reports</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Home Page</asp:LinkButton>
            <br />
        </div>
    </form>
</body>
</html>

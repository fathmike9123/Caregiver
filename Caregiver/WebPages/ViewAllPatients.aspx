<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllPatients.aspx.cs" Inherits="Caregiver.Web_Pages.ViewAllPatients" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/main.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" class="btn btn-light">Return to Home Page</asp:LinkButton>
        </nav>

        <br />
        <h1 class="display-1 text-center">View All Patients</h1>

        <div>
            <br />
            <br />
            <div class="container-fluid background-menu">
                <div class="card-deck" ID="PlaceHolder2" runat="server">
                </div>
            </div>
            <%--<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>--%>
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lbReports" runat="server" OnClick="lbReports_Click">View Patient Reports</asp:LinkButton>
            <br />
            <br />

            <br />
        </div>
    </form>
    <script type="text/javascript" src="../js/bootstrap.min.js" />
    <script type="text/javascript" src="../js/bootstrap.js" />
</body>
</html>

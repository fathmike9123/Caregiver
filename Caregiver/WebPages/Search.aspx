<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Caregiver.Web_Pages.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <style>
        .background-menu {
            text-align: center;
            padding-left: 20%;
            padding-right: 20%;
            padding-top: 5%;
        }
    </style>
    <script type="text/javascript" src="../js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.mask.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" class="btn btn-light">Return to Home Page</asp:LinkButton>
        </nav>

        <br />
        <br />
        <h1 class="display-1 text-center">Search Patients</h1>

        <div class="background-menu">
            <div class="form-row">
                <div class="form-group col-md-2">
                    <div class="btn-group dropright">
                        <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged" class="btn btn-primary dropdown-toggle ">
                            <asp:ListItem Selected="True" Value="First Name">First Name</asp:ListItem>
                            <asp:ListItem>Last Name</asp:ListItem>
                            <asp:ListItem>Sex</asp:ListItem>
                            <asp:ListItem>City</asp:ListItem>
                            <asp:ListItem>Phone Number</asp:ListItem>
                            <asp:ListItem>Address</asp:ListItem>
                            <asp:ListItem>Province</asp:ListItem>
                            <asp:ListItem>Postal Code</asp:ListItem>
                            <asp:ListItem>Symptoms</asp:ListItem>
                            <asp:ListItem>History</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-md-8">
                    <asp:TextBox ID="tbText" runat="server" class="form-control"></asp:TextBox>
                    <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" class="form-control">
                        <asp:ListItem>ON</asp:ListItem>
                        <asp:ListItem>AB</asp:ListItem>
                        <asp:ListItem>BC</asp:ListItem>
                        <asp:ListItem>MB</asp:ListItem>
                        <asp:ListItem>NB</asp:ListItem>
                        <asp:ListItem>NL</asp:ListItem>
                        <asp:ListItem>NT</asp:ListItem>
                        <asp:ListItem>NS</asp:ListItem>
                        <asp:ListItem>NU</asp:ListItem>
                        <asp:ListItem>PE</asp:ListItem>
                        <asp:ListItem>QC</asp:ListItem>
                        <asp:ListItem>SK</asp:ListItem>
                        <asp:ListItem>YT</asp:ListItem>
                    </asp:DropDownList>
                    <div id="divRdbSex" runat="server" class="form-control" style="text-align: left">
                        <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-check" style="text-align: left">
                        <asp:RadioButtonList ID="rdbSymptoms" runat="server">
                            <asp:ListItem Value="1" Selected="True">Chest Pain</asp:ListItem>
                            <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                            <asp:ListItem Value="3">Numbness</asp:ListItem>
                            <asp:ListItem Value="4">Dizziness</asp:ListItem>
                            <asp:ListItem Value="5">Fever</asp:ListItem>
                            <asp:ListItem Value="6">Vomiting</asp:ListItem>
                            <asp:ListItem Value="7">Constant Urination</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-check" style="text-align: left">
                        <asp:RadioButtonList ID="rdbHistory" runat="server">
                            <asp:ListItem Value="1" Selected="True">Heart Disease</asp:ListItem>
                            <asp:ListItem Value="2">Smoking</asp:ListItem>
                            <asp:ListItem Value="3">Diabetes</asp:ListItem>
                            <asp:ListItem Value="4">High Blood Pressure</asp:ListItem>
                            <asp:ListItem Value="5">Stroke</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-secondary" />
                </div>
            </div>
            <br />
            <div id="warningMessage" runat="server" class="alert alert-warning" role="alert" style="display: none;">
                Invalid username & password.
            </div>
            <br />
            <br />
            <br />

        </div>
        <div style="padding-left: 5%; padding-right : 5%;">
            <asp:GridView ID="gridViewResult" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" class="table">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

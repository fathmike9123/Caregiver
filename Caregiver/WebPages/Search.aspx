<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Caregiver.Web_Pages.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Search Patient"></asp:Label>
            <br />
            <br />
            <br />
            <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged">
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
            <asp:TextBox ID="tbText" runat="server"></asp:TextBox>
            <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="cbSymptom" runat="server">
                <asp:ListItem Value="1">Chest Pain</asp:ListItem>
                <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                <asp:ListItem Value="3">Numbness</asp:ListItem>
                <asp:ListItem Value="4">Dizziness</asp:ListItem>
                <asp:ListItem Value="5">Fever</asp:ListItem>
                <asp:ListItem Value="6">Vomiting</asp:ListItem>
                <asp:ListItem Value="7">Constant Urination</asp:ListItem>
            </asp:CheckBoxList>
            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True">
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
            <asp:CheckBoxList ID="cblHistory" runat="server">
                <asp:ListItem Value="1">Heart Disease</asp:ListItem>
                <asp:ListItem Value="2">Smoking</asp:ListItem>
                <asp:ListItem Value="3">Diabetes</asp:ListItem>
                <asp:ListItem Value="4">High Blood Pressure</asp:ListItem>
                <asp:ListItem Value="5">Stroke</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
            <br />
            <br />
            Results:<asp:GridView ID="gridViewResult" runat="server">
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Home Page</asp:LinkButton>
            <br />
        </div>
    </form>
</body>
</html>

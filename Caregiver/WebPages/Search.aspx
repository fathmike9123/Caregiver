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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ForeColor="Red"></asp:RegularExpressionValidator>
            <br />
            <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="First Name">First Name</asp:ListItem>
                <asp:ListItem>Last Name</asp:ListItem>
                <asp:ListItem>City</asp:ListItem>
                <asp:ListItem>Phone Number</asp:ListItem>
                <asp:ListItem>Symptoms</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="tbText" runat="server"></asp:TextBox>
            <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone"></asp:TextBox>
            <asp:CheckBoxList ID="cbSymptom" runat="server">
                <asp:ListItem Value="1">Chest Pain</asp:ListItem>
                <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                <asp:ListItem Value="3">Numbness</asp:ListItem>
                <asp:ListItem Value="4">Dizziness</asp:ListItem>
                <asp:ListItem Value="5">Fever</asp:ListItem>
                <asp:ListItem Value="6">Vomiting</asp:ListItem>
                <asp:ListItem Value="7">Constant Urination</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <br />
            <br />
            Results:<asp:GridView ID="gridViewResult" runat="server">
            </asp:GridView>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Home Page</asp:LinkButton>
            <br />
        </div>
    </form>
</body>
</html>

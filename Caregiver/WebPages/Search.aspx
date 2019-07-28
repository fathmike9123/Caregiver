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
            <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="First Name">First Name</asp:ListItem>
                <asp:ListItem>Last Name</asp:ListItem>
                <asp:ListItem>City</asp:ListItem>
                <asp:ListItem>Phone Number</asp:ListItem>
                <asp:ListItem>Symptoms</asp:ListItem>
            </asp:DropDownList>
&nbsp;<br />
            First Name:
            <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            <br />
            <br />
            Last Name:
            <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
            <br />
            <br />
            City: <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
            <br />
            <br />
            Phone Number:
            <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone"></asp:TextBox>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ForeColor="Red"></asp:RegularExpressionValidator>
            <br />
            <br />
            Symptoms:<asp:CheckBoxList ID="cbSymptom" runat="server" AutoPostBack="True">
                <asp:ListItem>Symptom1</asp:ListItem>
                <asp:ListItem>Symptom2</asp:ListItem>
                <asp:ListItem>Symptom3</asp:ListItem>
                <asp:ListItem>Symptom4</asp:ListItem>
                <asp:ListItem>Symptom5</asp:ListItem>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePatient.aspx.cs" Inherits="Caregiver.Web_Pages.CreatePatient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Create New Patient"></asp:Label>
            <br />
            <br />
            First Name:
            <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            <br />
            Last Name:
            <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
            <br />
            <br />
            Sex: <asp:RadioButtonList ID="rdbSex" runat="server">
                <asp:ListItem Selected="True">Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            Date of Birth (YYYY-MM-DD):
            <asp:TextBox ID="tbDob" runat="server"></asp:TextBox>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbDob" ErrorMessage="Invalid date format (YYYY-MM-DD)" ForeColor="Red" ValidationExpression="([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))"></asp:RegularExpressionValidator>
            <br />
            <br />
            History:<asp:CheckBoxList ID="cblHistory" runat="server">
                <asp:ListItem Value="1">Heart Disease</asp:ListItem>
                <asp:ListItem Value="2">Smoking</asp:ListItem>
                <asp:ListItem Value="3">Diabetes</asp:ListItem>
                <asp:ListItem Value="4">High Blood Pressure</asp:ListItem>
                <asp:ListItem Value="5">Stroke</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <br />
            Address:<asp:TextBox ID="tbAddress" runat="server"></asp:TextBox>
            <br />
            City:<asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
            <br />
            Province:<asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True">
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
            <br />
            Postal Code:
            <asp:TextBox ID="tbPostalCode" runat="server"></asp:TextBox>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbPostalCode" ErrorMessage="Invalid postal code format." ForeColor="Red" ValidationExpression="([ABCEGHJKLMNPRSTVXYabcdefghijklmnopqrstuvwxyz][0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz])\ ?([0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz][0-9])"></asp:RegularExpressionValidator>
            <br />
            <br />
            Phone Number:
            <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone"></asp:TextBox>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ForeColor="Red" ValidationExpression="^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$"></asp:RegularExpressionValidator>
            <br />
            <br />
            Symptoms:<asp:CheckBoxList ID="cblSymptom" runat="server">
                <asp:ListItem Value="1">Chest Pain</asp:ListItem>
                <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                <asp:ListItem Value="3">Numbness</asp:ListItem>
                <asp:ListItem Value="4">Dizziness</asp:ListItem>
                <asp:ListItem Value="5">Fever</asp:ListItem>
                <asp:ListItem Value="6">Vomiting</asp:ListItem>
                <asp:ListItem Value="7">Constant Urination</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <br />
            <asp:LinkButton ID="lbAdd" runat="server" OnClick="lbAdd_Click">Add New Patient</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Home Page</asp:LinkButton>
        </div>
    </form>
</body>
</html>

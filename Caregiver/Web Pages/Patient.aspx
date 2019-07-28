<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="Caregiver.Web_Pages.Patient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Patient Page"></asp:Label>
            <br />
            <br />
            Image of Patient:<br />
            <asp:Image ID="imgUser" runat="server" Height="164px" Width="236px" />
            <br />
            <br />
            First Name:
            <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            <br />
            Last Name:
            <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
            <br />
            <br />
            Sex:
            <asp:TextBox ID="tbSex" runat="server"></asp:TextBox>
            <br />
            Date of Birth:
            <asp:TextBox ID="tbDob" runat="server"></asp:TextBox>
            &nbsp;<br />
            <br />
            History:<asp:CheckBoxList ID="CheckBoxList1" runat="server">
                <asp:ListItem>Heart Disease</asp:ListItem>
                <asp:ListItem>Smoking</asp:ListItem>
                <asp:ListItem>Diabetes</asp:ListItem>
                <asp:ListItem>High Blood Pressure</asp:ListItem>
                <asp:ListItem>Stroke</asp:ListItem>
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
            &nbsp;<br />
            Phone Number:
            <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone"></asp:TextBox>
            &nbsp;<br />
            <br />
            Symptoms:<asp:CheckBoxList ID="cbSymptom" runat="server">
                <asp:ListItem>Chest Pain</asp:ListItem>
                <asp:ListItem>Shortness of Breath</asp:ListItem>
                <asp:ListItem>Numbness</asp:ListItem>
                <asp:ListItem>Dizziness</asp:ListItem>
                <asp:ListItem>Fever</asp:ListItem>
                <asp:ListItem>Vomiting</asp:ListItem>
                <asp:ListItem Value="Constant Urination"></asp:ListItem>
            </asp:CheckBoxList>
            <br />
            Diagnosis:
            <asp:Label ID="lblDiagnosis" runat="server" Text="Unknown Diagnosis"></asp:Label>
            <br />
            <br />
            <asp:Button ID="tbEdit" runat="server" Text="Edit Information" />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to View All Patients</asp:LinkButton>
        </div>
    </form>
</body>
</html>

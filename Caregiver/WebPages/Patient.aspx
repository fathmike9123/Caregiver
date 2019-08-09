<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="Caregiver.Web_Pages.Patient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.mask.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tbDob').mask('0000-00-00');
            $('#tbPhoneNum').mask('0000000000');
            $('#tbPostalCode').mask('S0S0S0');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 1%; text-align: center">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" class="alert alert-danger" runat="server" ControlToValidate="tbPostalCode" ErrorMessage="Invalid postal code format." ValidationExpression="([ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyz][0-9][ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyz])\ ?([0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz][0-9])" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" class="alert alert-danger" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ValidationExpression="^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" class="alert alert-danger" runat="server" ControlToValidate="tbDob" ErrorMessage="Invalid date inputted." ValidationExpression="([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

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
            <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            Date of Birth:
            <asp:TextBox ID="tbDob" runat="server"></asp:TextBox>
            &nbsp;<br />
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
            &nbsp;<br />
            Phone Number:
            <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone"></asp:TextBox>
            &nbsp;<br />
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
            <asp:Button ID="btnDiagnose" runat="server" Text="Diagnose Patient" OnClick="btn_Diagnose" />
            <br />
            <br />
            Diagnosis:
            <asp:Label ID="lblDiagnosis" runat="server" Text="Unknown Diagnosis"></asp:Label>
            <br />
            <asp:Label ID="lbl1" runat="server" Text="lbl1"></asp:Label>
            <br />
            <asp:Label ID="lbl2" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="lbl3" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="lbl4" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="btnEdit" runat="server" Text="Edit Information" OnClick="btnEdit_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save Information" OnClick="btnSave_Click" />
            <br />
            <asp:Label ID="lblUpdateResult" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to View All Patients</asp:LinkButton>
        </div>
    </form>
</body>
</html>

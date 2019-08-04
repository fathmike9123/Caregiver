<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePatient.aspx.cs" Inherits="Caregiver.Web_Pages.CreatePatient" %>

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
    <script type="text/javascript" src="../js/jquery.mask.js" ></script>
    <script type="text/javascript" src="../js/bootstrap.min.js" ></script>
    <script type="text/javascript" src="../js/bootstrap.js" ></script>
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
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" class="btn btn-light">Return to Home Page</asp:LinkButton>
        </nav>

        <br />
        <br />
        <h1 class="display-1 text-center">Create New Patient</h1>
        <div class="background-menu">
            <div>
                <div id="warningMessage" runat="server" class="alert alert-warning" role="alert" style="display: none;">
                    Invalid username & password.
                </div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" class="alert alert-danger" runat="server" ControlToValidate="tbPostalCode" ErrorMessage="Invalid postal code format." ValidationExpression="([ABCEGHJKLMNPRSTVXYabcdefghijklmnopqrstuvwxyz][0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz])\ ?([0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz][0-9])" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" class="alert alert-danger" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ValidationExpression="^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" class="alert alert-danger" runat="server" ControlToValidate="tbDob" ErrorMessage="Invalid date inputted." ValidationExpression="([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))" Display="Dynamic"></asp:RegularExpressionValidator>

            </div>
            <br />

            <div class="form-row">
                <div class="form-group col-md-4">
                    <label>First Name</label>
                    <asp:TextBox ID="tbFirstName" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label>Last Name</label>
                    <asp:TextBox ID="tbLastName" runat="server" class="form-control" placeholder="Last Name"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label>Sex</label>
                    <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal" class="form-control" align="center">
                        <asp:ListItem Selected="True">Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>









            <div class="form-group">
                <label for="inputAddress">Date of Birth (YYYY-MM-DD):</label>
                <asp:TextBox ID="tbDob" runat="server" class="form-control" placeholder="Date of Birth"></asp:TextBox>

            </div>


            <div class="form-group">
                <label for="inputAddress">Phone Number</label>
                <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone" class="form-control" placeholder="Phone Number"></asp:TextBox>

            </div>

            <div class="form-group">
                <label for="inputAddress">Address</label>
                <asp:TextBox ID="tbAddress" runat="server" class="form-control" placeholder="Address"></asp:TextBox>
            </div>


            <div class="form-row">
                <div class="form-group col-md-5">
                    <label for="inputCity">City</label>
                    <asp:TextBox ID="tbCity" runat="server" class="form-control" placeholder="City"></asp:TextBox>
                </div>
                <div class="form-group col-md-2">
                    <label for="inputState">Province</label>
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
                </div>
                <div class="form-group col-md-5">
                    <label for="inputZip">Postal Code</label>
                    <asp:TextBox ID="tbPostalCode" runat="server" class="form-control" placeholder="Postal Code"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="form-group row">
                <div class="col-sm-2">History:</div>
                <div class="col-sm-4">
                    <div class="form-check">
                        <asp:CheckBoxList ID="cblHistory" runat="server" Style="text-align: left">
                            <asp:ListItem Value="1">Heart Disease</asp:ListItem>
                            <asp:ListItem Value="2">Smoking</asp:ListItem>
                            <asp:ListItem Value="3">Diabetes</asp:ListItem>
                            <asp:ListItem Value="4">High Blood Pressure</asp:ListItem>
                            <asp:ListItem Value="5">Stroke</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-sm-2">Symptoms:</div>
                <div class="col-sm-4">
                    <div class="form-check" style="text-align: left">
                        <asp:CheckBoxList ID="cblSymptom" runat="server">
                            <asp:ListItem Value="1">Chest Pain</asp:ListItem>
                            <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                            <asp:ListItem Value="3">Numbness</asp:ListItem>
                            <asp:ListItem Value="4">Dizziness</asp:ListItem>
                            <asp:ListItem Value="5">Fever</asp:ListItem>
                            <asp:ListItem Value="6">Vomiting</asp:ListItem>
                            <asp:ListItem Value="7">Constant Urination</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <asp:LinkButton ID="lbAdd" runat="server" OnClick="lbAdd_Click" class="btn btn-primary">Add New Patient</asp:LinkButton>
        </div>
        <br />
        <br />

    </form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Caregiver.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <style>
        .background-menu {
            text-align: center;
            padding: 7%;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    </style>

</head>
<body>
    <br />
    <div class="container background-menu shadow-lg ">
        <form id="form1" runat="server">
            <img class="img-fluid mx-auto " src="../Images/Caregiver Logo.png" />
            <div>
                <br />
                <h1 class="display-4">Sign In</h1>
                <br />
                <div ID="warningMessage" runat="server" class="alert alert-warning" role="alert">
                    Invalid username & password.
                </div>
                <br />
                <br />
                <div class="form-group">
                    <h6>Email address:</h6>
                    <asp:TextBox class="form-control col-auto" placeholder="Enter email here" ID="tbEmail" runat="server"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator class="alert alert-danger" ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail" ErrorMessage="Email must be a valid format." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <br />
                <div class="form-group">
                    <h6>Password:</h6>
                    <asp:TextBox class="form-control" placeholder="Enter password here" ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <br />
                <asp:LinkButton ID="lbSignIn" runat="server" OnClick="lbSignIn_Click" class="btn btn-primary">Sign In</asp:LinkButton>
            </div>
        </form>
    </div>

    <script type="text/javascript" src="../js/bootstrap.min.js" />
    <script type="text/javascript" src="../js/bootstrap.js" />
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseAccess.aspx.cs" Inherits="Caregiver.Web_Pages.DatabaseAccess" %>

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
            padding-top: 2%;
        }

        .update-section {
            text-align: center;
            margin-left: 20%;
            margin-right: 20%;
            padding: 2%;
        }
    </style>
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
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" class="btn btn-light">Return to Home Page</asp:LinkButton>
        </nav>
        <br />
        <div style="padding-left:1%;text-align:center">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" class="alert alert-danger" runat="server" ControlToValidate="tbPostalCode" ErrorMessage="Invalid postal code format." ValidationExpression="([ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyz][0-9][ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyz])\ ?([0-9][ABCEGHJKLMNPRSTVWXYZabcdefghijklmnopqrstuvwxyz][0-9])" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" class="alert alert-danger" runat="server" ControlToValidate="tbPhoneNum" ErrorMessage="Invalid phone number format." ValidationExpression="^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" class="alert alert-danger" runat="server" ControlToValidate="tbDob" ErrorMessage="Invalid date inputted." ValidationExpression="([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <br />
        <h1 class="display-1 text-center">Database Access</h1>

        <div class="background-menu">
            <asp:Button ID="btnUsers" runat="server" OnClick="btnUsers_Click" Text="View Users" class="btn btn-primary" />
            <asp:Button ID="btnPatients" runat="server" OnClick="btnPatients_Click" Text="View Patients" class="btn btn-primary" />
            <asp:Button ID="btnPatientHistory" runat="server" OnClick="btnPatientHistory_Click" Text="View Patient History" class="btn btn-primary" />
            <asp:Button ID="btnPatientSymptom" runat="server" OnClick="btnPatientSymptom_Click" Text="View Patient Symptoms" class="btn btn-primary" />
            <br />

            <br />

        </div>

        <!-- <div> for the Users -->
        <div runat="server" id="divUsers" style="display: none;" class="update-section border">
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label>Email</label>
                    <asp:TextBox ID="tbEmail" runat="server" ReadOnly="True" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label>Password</label>
                    <asp:TextBox ID="tbPassword" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label>Is Admin</label>
                    <asp:CheckBox ID="cbIsAdmin" runat="server" class="form-control" />
                </div>

            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Button ID="btnUpdateUser" runat="server" OnClick="btnUpdateUser_Click" Text="Update User" class="btn btn-secondary" />
                </div>
                <div class="form-group col-md-6">
                    <asp:Button ID="btnDeleteUser" runat="server" OnClick="btnDeleteUser_Click" Text="Delete User" class="btn btn-secondary" />
                </div>
            </div>
        </div>


        <!-- <div> for the PatientHistory -->
        <div runat="server" id="divPatientHis" class="update-section border">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label>Name</label>
                    <asp:TextBox ID="tbFullNameHis" runat="server" ReadOnly="True" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>History</label>
                    <asp:DropDownList ID="ddlHistory" runat="server" class="form-control">
                        <asp:ListItem Value="1">Heart Disease</asp:ListItem>
                        <asp:ListItem Value="2">Smoking</asp:ListItem>
                        <asp:ListItem Value="3">Diabetes</asp:ListItem>
                        <asp:ListItem Value="4">High Blood Pressure</asp:ListItem>
                        <asp:ListItem Value="5">Stroke</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Button ID="btnUpdateHistory" runat="server" Text="Update History" OnClick="btnUpdateHistory_Click" class="btn btn-secondary" />
                </div>
                <div class="form-group col-md-6">
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="Delete History" OnClick="btnDeleteHistory_Click" class="btn btn-secondary" />
                </div>
            </div>
        </div>


        <!-- <div> for the PatientSymptoms -->
        <div runat="server" id="divPatientSymp" class="update-section border">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label>Name</label>
                    <asp:TextBox ID="tbFullNameSymp" runat="server" ReadOnly="True" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>Symptoms</label>
                    <asp:DropDownList ID="ddlSymptom" runat="server" class="form-control">
                        <asp:ListItem Value="1">Chest Pain</asp:ListItem>
                        <asp:ListItem Value="2">Shortness of Breath</asp:ListItem>
                        <asp:ListItem Value="3">Numbness</asp:ListItem>
                        <asp:ListItem Value="4">Dizziness</asp:ListItem>
                        <asp:ListItem Value="5">Fever</asp:ListItem>
                        <asp:ListItem Value="6">Vomiting</asp:ListItem>
                        <asp:ListItem Value="7">Constant Urination</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Button ID="btnUpdateSymptom" runat="server" Text="Update Symptom" OnClick="btnUpdateSymptom_Click" class="btn btn-secondary" />
                </div>
                <div class="form-group col-md-6">
                    <asp:Button ID="btnDeleteSymptom" runat="server" Text="Delete Symptom" OnClick="btnDeleteSymptom_Click" class="btn btn-secondary" />
                </div>
            </div>
        </div>

        <!-- <div> for the Patients -->
        <div runat="server" id="divPatients" class="update-section border">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label>First Name</label>
                    <asp:TextBox ID="tbFirstName" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>Last Name</label>
                    <asp:TextBox ID="tbLastName" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label>Sex</label>
                    <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal" class="form-control">
                        <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group col-md-3">
                    <label>Date of Birth (YYYY-MM-DD)</label>
                    <asp:TextBox ID="tbDob" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>Address</label>
                    <asp:TextBox ID="tbAddress" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>City</label>
                    <asp:TextBox ID="tbCity" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>Province</label>
                    <asp:DropDownList ID="ddlProvince" runat="server" class="form-control">
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
                <div class="form-group col-md-6">
                    <label>Postal Code</label>
                    <asp:TextBox ID="tbPostalCode" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label>Phone Number</label>
                    <asp:TextBox ID="tbPhoneNum" runat="server" TextMode="Phone" class="form-control"></asp:TextBox>
                </div>

            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Button ID="btnUpdatePatient" runat="server" OnClick="btnUpdatePatient_Click" Text="Update Patient" class="btn btn-secondary" />
                </div>
                <div class="form-group col-md-6">
                    <asp:Button ID="btnDeletePatient" runat="server" OnClick="btnDeletePatient_Click" Text="Delete Patient" class="btn btn-secondary" />
                </div>
            </div>
        </div>

        <!-- Grid View -->
        <br />
        <br />
        <div style="padding-left: 5%; padding-right: 5%; text-align: center">
            <asp:GridView ID="grdResult" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" class="table">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select" CssClass="btn btn-success"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Caregiver.Web_Pages.Home" %>

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
        }
    </style>
    <script type="text/javascript" src="../js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.mask.js" ></script>
    <script type="text/javascript" src="../js/bootstrap.min.js" ></script>
    <script type="text/javascript" src="../js/bootstrap.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbSignOut" runat="server" OnClick="lbSignOut_Click" class="btn btn-light">Sign Out</asp:LinkButton>
        </nav>

        <br />
        <h1 class="display-1 text-center">Home Page</h1>



        <div class="container-fluid background-menu">
            <div class="card-deck">

                <div class="card shadow-sm p-3 mb-5 bg-white rounded">
                    <asp:LinkButton ID="lbViewAllPatients" runat="server" OnClick="lbViewAllPatients_Click" Style="text-decoration: none">
                        <img src="https://t3.ftcdn.net/jpg/00/21/75/28/500_F_21752821_B4rsSwMGCuVU40MRuJdjx0QwGNhLP3sQ.jpg" class="card-img-top"/>
                        <div class="card-body">
                            <h5 class="card-title">View All Patients</h5>
                            <p class="card-text">See all patients in the system.</p>
                        </div>
                    </asp:LinkButton>
                </div>


                <div class="card shadow-sm p-3 mb-5 bg-white rounded">
                    <asp:LinkButton ID="lbCreateNew" runat="server" OnClick="lbCreateNew_Click" Style="text-decoration: none">
                        <img src="https://t3.ftcdn.net/jpg/00/21/75/28/500_F_21752821_B4rsSwMGCuVU40MRuJdjx0QwGNhLP3sQ.jpg" class="card-img-top"/>
                        <div class="card-body">
                            <h5 class="card-title">Create New Patient</h5>
                            <p class="card-text">Add a new patient to the system.</p>
                        </div>
                    </asp:LinkButton>
                </div>



                <div class="card shadow-sm p-3 mb-5 bg-white rounded">
                    <asp:LinkButton ID="lbSearch" runat="server" OnClick="lbSearch_Click" Style="text-decoration: none">
                        <img src="https://t3.ftcdn.net/jpg/00/21/75/28/500_F_21752821_B4rsSwMGCuVU40MRuJdjx0QwGNhLP3sQ.jpg" class="card-img-top"/>
                        <div class="card-body">
                            <h5 class="card-title">Search for Patients</h5>
                            <p class="card-text">Search patients by a criteria.</p>
                        </div>
                    </asp:LinkButton>
                </div>


                <div id="databaseDiv" runat="server" class="card shadow-sm p-3 mb-5 bg-white rounded">
                    <asp:LinkButton ID="lbDatabase" runat="server" OnClick="lbDatabase_Click" Style="text-decoration: none">
                        <img src="https://t3.ftcdn.net/jpg/00/21/75/28/500_F_21752821_B4rsSwMGCuVU40MRuJdjx0QwGNhLP3sQ.jpg" class="card-img-top"/>
                        <div class="card-body">
                            <h5 class="card-title">View Database</h5>
                            <p class="card-text">View records in the system.</p>
                        </div>
                    </asp:LinkButton>
                </div>

            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Caregiver.Web_Pages.Reports" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/main.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <style>
        .background-menu {
            text-align: center;
            padding : 4% 7% 0% 7%;
        }
    </style>
    <script type="text/javascript" src="../js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.mask.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-light bg-light  ">
            <img src="../Images/Caregiver Logo.png" height="30" alt="" />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" class="btn btn-light">Return to View All Patients</asp:LinkButton>
        </nav>

        <br /><br />
        <h1 class="display-1 text-center">Reports & Charts</h1>

        <div class="background-menu">
            <asp:Button ID="btnHistory" runat="server" OnClick="btnHistory_Click" Text="By History" class="btn btn-primary" />
            <asp:Button ID="btnSymptom" runat="server" Text="By Symptom" OnClick="btnSymptom_Click" class="btn btn-primary" />
            <asp:Button ID="btnSex" runat="server" OnClick="btnSex_Click" Text="By Sex" Width="89px" class="btn btn-primary" />
            <br /><br /><br />
            <asp:Chart ID="chartReport" runat="server" Height="493px" Width="851px" DataSourceID="SqlDataSource1" BorderlineColor="Transparent" Palette="Bright" CssClass="auto-style1">
                <Series>
                    <asp:Series Name="Series1" XValueMember="XValue" YValueMembers="YValue">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CaregiverConnectionString %>" SelectCommand="SELECT h.Name as XValue, COUNT(*) as YValue
FROM PatientHistory p INNER JOIN History h ON p.HistoryId = h.HistoryId
GROUP BY h.Name;"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>

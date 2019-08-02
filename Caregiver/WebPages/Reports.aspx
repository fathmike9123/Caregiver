<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Caregiver.Web_Pages.Reports" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Reports Page"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnHistory" runat="server" OnClick="btnHistory_Click" Text="By History" />
&nbsp;<asp:Button ID="btnSymptom" runat="server" Text="By Symptom" OnClick="btnSymptom_Click" />
            &nbsp;<asp:Button ID="btnSex" runat="server" OnClick="btnSex_Click" Text="By Sex" Width="89px" />
            <br />
            <asp:Chart ID="chartReport" runat="server" Height="455px" Width="579px" DataSourceID="SqlDataSource1">
                <series>
                    <asp:Series Name="Series1" XValueMember="XValue" YValueMembers="YValue">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CaregiverConnectionString %>" SelectCommand="SELECT h.Name as XValue, COUNT(*) as YValue
FROM PatientHistory p INNER JOIN History h ON p.HistoryId = h.HistoryId
GROUP BY h.Name;"></asp:SqlDataSource>
            <br />
            <br />
            <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to View All Patients</asp:LinkButton>
        </div>
    </form>
</body>
</html>

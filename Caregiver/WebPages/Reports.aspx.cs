using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

namespace Caregiver.Web_Pages {
    public partial class Reports : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}
                LoadHistoryDefault();
            }
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        protected void btnHistory_Click(object sender, EventArgs e) {
            LoadHistoryDefault();
        }

        private void LoadHistoryDefault() {
            SqlDataSource1.SelectCommand = "SELECT h.Name as XValue, COUNT(*) as YValue " +
                " FROM PatientHistory p INNER JOIN History h ON p.HistoryId = h.HistoryId" +
                " GROUP BY h.Name";
            chartReport.ChartAreas[0].AxisX.Title = "History Traits";
            chartReport.ChartAreas[0].AxisY.Title = "Number of Patients";
            chartReport.Titles.Add(new Title("Number of Patients per History Trait"));
            SqlDataSource1.Select(new DataSourceSelectArguments());
        }
        protected void btnSymptom_Click(object sender, EventArgs e) {
            SqlDataSource1.SelectCommand = "SELECT s.Name as XValue, COUNT(*) as YValue " +
                " FROM PatientSymptom p INNER JOIN Symptom s ON p.SymptomId = s.SymptomId" +
                " GROUP BY s.Name";
            chartReport.ChartAreas[0].AxisX.Title = "Symptoms";
            chartReport.ChartAreas[0].AxisY.Title = "Number of Patients";
            chartReport.Titles.Add(new Title("Number of Patients per Symptom"));
            SqlDataSource1.Select(new DataSourceSelectArguments());
        }

        protected void btnSex_Click(object sender, EventArgs e) {
            SqlDataSource1.SelectCommand = "SELECT Sex as XValue, COUNT(*) as YValue " +
                "FROM Patient " +
                "GROUP BY Sex " +
                "ORDER BY 1 DESC;";
            chartReport.ChartAreas[0].AxisX.Title = "Sex";
            chartReport.ChartAreas[0].AxisY.Title = "Number of Patients";
            chartReport.Titles.Add(new Title("Number of Patients per Sex"));
            SqlDataSource1.Select(new DataSourceSelectArguments());
        }
    }
}
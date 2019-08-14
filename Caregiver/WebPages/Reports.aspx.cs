using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Code Behind File for Reports.aspx page
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class Reports : System.Web.UI.Page {

        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }

                // Shows the report based on the number of patients with a given history
                LoadHistoryDefault();
            }
        }

        /// <summary>
        /// Return to ViewAllPatients page
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        /// <summary>
        /// Shows the report based on the number of patients with a given history
        /// </summary>
        protected void btnHistory_Click(object sender, EventArgs e) {
            LoadHistoryDefault();
        }

        /// <summary>
        /// Shows the report based on the number of patients with a given history
        /// </summary>
        private void LoadHistoryDefault() {
            SqlDataSource1.SelectCommand = "SELECT h.Name as XValue, COUNT(*) as YValue " +
                " FROM PatientHistory p INNER JOIN History h ON p.HistoryId = h.HistoryId" +
                " GROUP BY h.Name";
            chartReport.ChartAreas[0].AxisX.Title = "History Traits";
            chartReport.ChartAreas[0].AxisY.Title = "Number of Patients";
            chartReport.Titles.Add(new Title("Number of Patients per History Trait"));
            SqlDataSource1.Select(new DataSourceSelectArguments());
        }

        /// <summary>
        /// Shows the report based on the number of patients with a given symptom
        /// </summary>
        protected void btnSymptom_Click(object sender, EventArgs e) {
            SqlDataSource1.SelectCommand = "SELECT s.Name as XValue, COUNT(*) as YValue " +
                " FROM PatientSymptom p INNER JOIN Symptom s ON p.SymptomId = s.SymptomId" +
                " GROUP BY s.Name";
            chartReport.ChartAreas[0].AxisX.Title = "Symptoms";
            chartReport.ChartAreas[0].AxisY.Title = "Number of Patients";
            chartReport.Titles.Add(new Title("Number of Patients per Symptom"));
            SqlDataSource1.Select(new DataSourceSelectArguments());
        }

        /// <summary>
        /// Shows the report based on the number of patients with a given sex
        /// </summary>
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
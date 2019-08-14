using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Code Behind File for Search.aspx
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class Search : System.Web.UI.Page {

        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }
                // Only shows the Search textbox on initial load
                tbText.Style.Add("display", "inline");
                rdbSex.Style.Add("display", "none");
                rdbSymptoms.Style.Add("display", "none");
                ddlProvince.Style.Add("display", "none");
                rdbHistory.Style.Add("display", "none");
                divRdbSex.Style.Add("display", "none");
            } else {
                // Hides all the user prompts during post back
                tbText.Style.Add("display", "none");
                rdbSex.Style.Add("display", "none");
                divRdbSex.Style.Add("display", "none");
                rdbSymptoms.Style.Add("display", "none");
                ddlProvince.Style.Add("display", "none");
                rdbHistory.Style.Add("display", "none");
                warningMessage.Style.Add("display", "none");

                // Only shows the user prompt that was last chosen before post back
                DisplaySelected();
            }

        }

        /// <summary>
        /// Return to Home.aspx
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        /// <summary>
        /// Search for record(s) based on user input
        /// "toBeAppended" is to be added at the end of the SQL query statement 
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e) {
            string selectedValue = ddlChoice.SelectedValue;

            if (selectedValue == "First Name") {
                string toBeAppended = "FirstName LIKE '%'+@FirstName+'%'";
                SearchCriteria(toBeAppended, "@FirstName", tbText.Text);

            } else if (selectedValue == "Last Name") {
                string toBeAppended = "LastName LIKE '%'+@LastName+'%'";
                SearchCriteria(toBeAppended, "@LastName", tbText.Text);

            } else if (selectedValue == "Sex") {
                string toBeAppended = "Sex = @Sex";
                SearchCriteria(toBeAppended, "@Sex", rdbSex.SelectedValue);

            } else if (selectedValue == "City") {
                string toBeAppended = "City LIKE '%'+@City+'%'";
                SearchCriteria(toBeAppended, "@City", tbText.Text);

            } else if (selectedValue == "Phone Number") {
                string toBeAppended = "PhoneNum LIKE '%'+@PhoneNum+'%'";
                SearchCriteria(toBeAppended, "@PhoneNum", tbText.Text);

            } else if (selectedValue == "Address") {
                string toBeAppended = "Address LIKE '%'+@Address+'%'";
                SearchCriteria(toBeAppended, "@Address", tbText.Text);

            } else if (selectedValue == "Province") {
                string toBeAppended = "Province=@Province";
                SearchCriteria(toBeAppended, "@Province", ddlProvince.SelectedValue);

            } else if (selectedValue == "Postal Code") {
                string toBeAppended = "PostalCode LIKE '%'+@PostalCode+'%'";
                SearchCriteria(toBeAppended, "@PostalCode", tbText.Text);

            } else if (selectedValue == "Symptoms") {
                SearchLists("Symptom", rdbSymptoms);

            } else if (selectedValue == "History") {
                SearchLists("History", rdbHistory);
            }
        }

        /// <summary>
        /// Search through the database based on user's criteria (excludes Symptoms + History)
        /// </summary>
        private void SearchCriteria(string append, string paramName, string paramValue) {
            if (paramValue != "") {
                string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
                using (SqlConnection conn = new SqlConnection(conString)) {
                    try {
                        using (SqlCommand cmd = new SqlCommand()) {
                            conn.Open();
                            cmd.Connection = conn;

                            cmd.CommandText = "SELECT * FROM Patient WHERE " + append;
                            cmd.Parameters.AddWithValue(paramName, paramValue);
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.HasRows) {
                                // Shows results on the Grid View if there are results
                                gridViewResult.DataSource = reader;
                                gridViewResult.DataBind();
                            } else {
                                // Shows error message when there are no results and hides the grid view
                                warningMessage.Style.Add("display", "inline");
                                warningMessage.InnerHtml = "There are no results!";
                                ClearGridView();
                            }
                            reader.Close();
                        }
                    } catch (SqlException ex) {
                        Response.Write("<script>alert('An error has occured with the database.');</script>");
                    }
                }
            } else {
                warningMessage.Style.Add("display", "inline");
                warningMessage.InnerHtml = "Search criteria must not be empty!";
            }
        }

        /// <summary>
        /// Search through the database based on user's selected history or symptom
        /// </summary>
        private void SearchLists(string tableName, RadioButtonList rdb) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(conString)) {
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;

                        string fieldName = "";
                        string query = "SELECT DISTINCT Patient.PatientId, FirstName, LastName, Sex, Birthday, Address, City, Province, PostalCode, PhoneNum ";

                        if (tableName == "Symptom") {
                            // Updates query to search for PatientSymptom record
                            query += "FROM Patient INNER JOIN PatientSymptom " +
                                "ON Patient.PatientId = PatientSymptom.PatientId ";
                            fieldName = "SymptomId";
                        } else {
                            // Updates query to search for PatientHistory record
                            query += "FROM Patient INNER JOIN PatientHistory " +
                                "ON Patient.PatientId = PatientHistory.PatientId ";
                            fieldName = "HistoryId";
                        }
                        query += "WHERE " + fieldName + "='"+rdb.SelectedValue+"'";
                        
                        cmd.CommandText = query;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows) {
                            // Shows results on the Grid View if there are results
                            gridViewResult.DataSource = reader;
                            gridViewResult.DataBind();
                        } else {
                            // Shows error message when there are no results and hides the grid view
                            warningMessage.Style.Add("display", "inline");
                            warningMessage.InnerHtml = "There are no results!";
                            ClearGridView();
                        }
                        reader.Close();
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");

                }
            }
        }

        /// <summary>
        /// Displays proper user inputs based on the drop down selected
        /// </summary>
        protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e) {
            DisplaySelected();
            tbText.Text = "";
            ClearGridView();
        }

        /// <summary>
        /// Removes contents of the Grid VIew
        /// </summary>
        private void ClearGridView() {
            gridViewResult.DataSource = null;
            gridViewResult.DataBind();
        }

        /// <summary>
        /// Displays the user input fields based on what was selected in the drop down menu
        /// </summary>
        private void DisplaySelected() {
            if (ddlChoice.SelectedValue == "Sex") {
                rdbSex.Style.Add("display", "inline");
                divRdbSex.Style.Add("display", "block");
            } else if (ddlChoice.SelectedValue == "Province") {
                ddlProvince.Style.Add("display", "inline");
            } else if (ddlChoice.SelectedValue == "Symptoms") {
                rdbSymptoms.Style.Add("display", "inline");
            } else if (ddlChoice.SelectedValue == "History")
                rdbHistory.Style.Add("display", "inline");
            else {
                tbText.Style.Add("display", "inline");
            }
        }
    }
}
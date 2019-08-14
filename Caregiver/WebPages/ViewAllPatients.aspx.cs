using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Caregiver.Classes;

/// <author>Ryan Haire</author>
/// <summary>
/// Code Behind File for ViewAllPatients.aspx
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class ViewAllPatients : System.Web.UI.Page {




        private PatientIndexer indexer = new PatientIndexer();



        /// <author>Stefano Gregor& Ryan Haire</author>
        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered. 
        /// Also, the list of patients are queried from the database and displayed on to the screen as buttons
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}
            }
            SetIndexer();
            CreatePatientButtons();
        }

        private void CreatePatientButtons() {
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM Patient;";

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {

                            // Outer div to hold the card
                            Panel divOuter = new Panel();
                            divOuter.CssClass = "card shadow-sm p - 3 mb - 5 bg - white rounded";
                            divOuter.Style.Add("text-align", "center");

                            // LinkButton to direct the user to the next web page
                            LinkButton btn = new LinkButton();
                            divOuter.Controls.Add(btn);

                            // Image of the patient (depending on sex)
                            Image img = new Image();
                            btn.Controls.Add(img);
                            img.Style.Add("padding", "10%");
                            img.CssClass = "card-img-top";

                            // Inner div to hold text of the card
                            Panel divInner = new Panel();
                            divInner.CssClass = "card-body";
                            btn.Controls.Add(divInner);

                            // Text inside inner div that shows patient's name
                            Label innerHtml = new Label();
                            innerHtml.Text = reader[1] + " " + reader[2];
                            innerHtml.CssClass = "card-title";
                            divInner.Controls.Add(innerHtml);


                            // Sets the appropariate picture by patient's sex
                            if ((string)reader[3] == "M") {
                                img.ImageUrl = "/Images/Male.png";
                            } else {
                                img.ImageUrl = "/Images/Female.png";
                            }

                            // Assigns the ID for the patient
                            btn.ID = reader[0].ToString();

                            // Assigns the event handler for each button programmatically
                            btn.Click += btnPatient_Click;

                            // Puts the card in the UI
                            PlaceHolder2.Controls.Add(divOuter);
                        }
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }
        /// <author>Stefano Gregor Unlayao</author>
        /// <summary>
        /// Event Handler for each dynamically-created buttons
        /// This sets the PatientId session variable based on which patient was selected
        /// </summary>
        private void btnPatient_Click(object sender, EventArgs e) {
            int patientId = Convert.ToInt32(((LinkButton)sender).ID);
            SetSelectedPatient(patientId);
            //Session["PatientId"] = patientId;

        }

        private void SetSelectedPatient(int index) {
            Classes.Patient selectedPatient = this.indexer[index];
            if (selectedPatient != null) {
                Session["SelectedPatient"] = selectedPatient;
                Server.Transfer("Patient.aspx");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetIndexer(){
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = "server=(local);database=Caregiver;integrated security=SSPI;";
                using (SqlCommand cmd = new SqlCommand()) {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Patient";

                    SqlDataReader reader = cmd.ExecuteReader();

                    

                    // Get all of the patient's information
                    while (reader.Read()) {
                        Classes.Patient patient = new Classes.Patient();
                        patient.Id = Convert.ToInt32(reader[0]);
                        patient.FirstName = (string)reader[1];
                        patient.LastName = (string)reader[2];
                        patient.Sex = Convert.ToChar(reader[3]);
                        patient.Dob = reader[4].ToString();
                        patient.Address = (string)reader[5];
                        patient.City = (string)reader[6];
                        patient.Province = (string)reader[7];
                        patient.PostalCode = (string)reader[8];
                        patient.PhoneNum = (string)reader[9];
                        indexer.AddPatient(patient);
                    } // end of patient table info

                    reader.Close();

                    // Get all of the patient's history
                    cmd.CommandText = "SELECT Patient.PatientId, h.Name FROM History h JOIN PatientHistory " +
                                    "ON h.HistoryId = PatientHistory.HistoryId JOIN Patient " +
                                    "ON Patient.PatientId = PatientHistory.PatientId";


                    reader = cmd.ExecuteReader();
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            Classes.Patient patient = indexer[Convert.ToInt32(reader[0])];
                            patient.History.Add(reader[1].ToString());
                        }
                    }
                    reader.Close();

                    // Get all of the patient's symptoms
                    cmd.CommandText = "SELECT Patient.PatientId, s.Name FROM Symptom s JOIN PatientSymptom " +
                        "ON s.SymptomId = PatientSymptom.SymptomId JOIN Patient " +
                        "ON Patient.PatientId = PatientSymptom.PatientId ";

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            Classes.Patient patient = indexer[Convert.ToInt32(reader[0])];
                            patient.Symptoms.Add(reader[1].ToString());
                        }
                    }
                    reader.Close();
                }
            }
        }
        /// <summary>
        /// Returns to Home.aspx
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        /// <summary>
        /// Returns to Reports.aspx
        /// </summary>
        protected void lbReports_Click(object sender, EventArgs e) {
            Server.Transfer("Reports.aspx");
        }
    }
}
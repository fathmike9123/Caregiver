using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/*
 * “How to get values of selected items in CheckBoxList with foreach in ASP.NET C#?,” Stack Overflow, 01-Mar-1964. [Online]. 
 * Available: https://stackoverflow.com/questions/18924147/how-to-get-values-of-selected-items-in-checkboxlist-with-foreach-in-asp-net-c. 
 * [Accessed: 28-Jul-2019].
 *
 * “Get PK Id of newly inserted row using C#,” The Official Forums for Microsoft ASP.NET. [Online]. 
 * Available: https://forums.asp.net/t/2132465.aspx?Get PK Id of newly inserted row using C . 
 * [Accessed: 28-Jul-2019].
 */

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Code Behind File for the CreatePatient.aspx
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class CreatePatient : System.Web.UI.Page {

        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }
            }
        }

        /// <summary>
        /// Creates new patient based on user input
        /// </summary>
        protected void lbAdd_Click(object sender, EventArgs e) {
            // All inputs must be filled in before creating patient
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbDob.Text == "" || tbPhoneNum.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbPostalCode.Text == "") {
                warningMessage.Style.Add("display", "inline");
                warningMessage.InnerHtml = "You must fill in all text boxes!";
            } else {
                // Set patient's information
                string fName = tbFirstName.Text;
                string lName = tbLastName.Text;
                char sex = rdbSex.SelectedIndex == 0 ? 'M' : 'F';
                string dob = tbDob.Text;
                string address = tbAddress.Text;
                string city = tbCity.Text;
                string province = ddlProvince.SelectedValue;
                string postalCode = tbPostalCode.Text.ToUpper();
                string phoneNum = tbPhoneNum.Text;

                // Sets patient's history (only selected checkboxes)
                List<string> history = new List<string>();
                foreach (ListItem item in cblHistory.Items) {
                    if (item.Selected) {
                        history.Add(item.Value);
                    }
                }                

                // Sets patient's symptoms (only selected checkboxes)
                List<string> symptoms = new List<string>();
                foreach (ListItem item in cblSymptom.Items) {
                    if (item.Selected) {
                        symptoms.Add(item.Value);
                    }
                }

                // Create new Patient object
                Classes.Patient patient = new Classes.Patient(fName, lName, sex, dob);
                patient.SetLocation(address, city, province, postalCode, phoneNum);
                patient.History = history;
                patient.Symptoms = symptoms;

                AddNewPatient(patient);
                Server.Transfer("Home.aspx");
            }
        }

        /// <summary>
        /// Add new patient to the database
        /// </summary>
        private void AddNewPatient(Classes.Patient patient) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(conString)) {
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;

                        // Insert new patient record to Patient table
                        cmd.CommandText = "INSERT INTO Patient (FirstName,LastName,Sex,Birthday," +
                                          "Address,City,Province,PostalCode,PhoneNum)" +
                                          "VALUES(@FirstName,@LastName,@Sex,@Birthday," +
                                          "@Address,@City,@Province,@PostalCode,@PhoneNum);" +
                                          "SELECT SCOPE_IDENTITY()";

                        cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                        cmd.Parameters.AddWithValue("@Sex", patient.Sex);
                        cmd.Parameters.AddWithValue("@Birthday", patient.Dob);
                        cmd.Parameters.AddWithValue("@Address", patient.Address);
                        cmd.Parameters.AddWithValue("@City", patient.City);
                        cmd.Parameters.AddWithValue("@Province", patient.Province);
                        cmd.Parameters.AddWithValue("@PostalCode", patient.PostalCode.ToUpper());
                        cmd.Parameters.AddWithValue("@PhoneNum", patient.PhoneNum);

                        // Since it's an auto-incremented primary key, we want to get its primary key to insert the symptoms + history
                        int patientID = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insert into PatientHistory table based on the PatientId above
                        foreach (string item in patient.History) {
                            cmd.CommandText = "INSERT INTO PatientHistory VALUES(@PatientID,@HistoryId)";
                            cmd.Parameters.AddWithValue("@PatientId", patientID);
                            cmd.Parameters.AddWithValue("@HistoryId", item);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        // Insert into PatientSymptom table based on the PatientId above
                        foreach (string item in patient.Symptoms) {
                            cmd.CommandText = "INSERT INTO PatientSymptom VALUES(@PatientID,@SymptomId)";
                            cmd.Parameters.AddWithValue("@PatientId", patientID);
                            cmd.Parameters.AddWithValue("@SymptomId", item);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");

                }
            }
        }

        /// <summary>
        /// Returns to Home.aspx
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }
    }
}

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
namespace Caregiver.Web_Pages {
    public partial class CreatePatient : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e) {
            string fName = tbFirstName.Text;
            string lName = tbLastName.Text;
            char sex;
            if (rdbSex.SelectedIndex == 0) {
                sex = 'M';
            } else {
                sex = 'F';
            }
            string dob = tbDob.Text;


            List<string> history = new List<string>();
            foreach (ListItem item in cblHistory.Items) {
                if (item.Selected) {
                    history.Add(item.Value);
                }
            }

            string address = tbAddress.Text;
            string city = tbCity.Text;
            string province = ddlProvince.SelectedValue;
            string postalCode = tbPostalCode.Text;
            string phoneNum = tbPhoneNum.Text;

            List<string> symptoms = new List<string>();
            foreach (ListItem item in cblSymptom.Items) {
                if (item.Selected) {
                    symptoms.Add(item.Value);
                }
            }

            Classes.Patient patient = new Classes.Patient(fName, lName, sex, dob);
            patient.SetLocation(address, city, province, postalCode, phoneNum);
            patient.History = history;
            patient.Symptoms = symptoms;

            DatabaseAccess db = new DatabaseAccess();
            AddNewPatient(patient);

            Server.Transfer("Home.aspx");
        }

        private void AddNewPatient(Classes.Patient patient) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(conString)) {
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        // Adds patient
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
                        cmd.Parameters.AddWithValue("@PostalCode", patient.PostalCode);
                        cmd.Parameters.AddWithValue("@PhoneNum", patient.PhoneNum);

                        int patientID = Convert.ToInt32(cmd.ExecuteScalar());

                        System.Diagnostics.Debug.WriteLine(patientID);

                        foreach (string item in patient.History) {
                            cmd.CommandText = "INSERT INTO PatientHistory VALUES(@PatientID,@HistoryId)";
                            cmd.Parameters.AddWithValue("@PatientId", patientID);
                            cmd.Parameters.AddWithValue("@HistoryId", item);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

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

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }
    }
}

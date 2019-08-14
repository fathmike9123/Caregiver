using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Code Behind File for DatabaseAccess.aspx
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class DatabaseAccess : System.Web.UI.Page {

        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered.
        /// Also, only allows admins to enter this page
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }

                if (!(bool)Session["IsAdmin"]) {
                    Server.Transfer("Home.aspx");
                }
            }
            HideDivs();
        }

        /// <summary>
        /// Returns to Home.aspx page
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        /// <summary>
        /// <div> tags in DatabaseAccess.aspx will be hidden from the user (which shows user prompts)
        /// </summary>
        private void HideDivs() {
            divPatients.Style.Add("display", "none");
            divUsers.Style.Add("display", "none");
            divPatientHis.Style.Add("display", "none");
            divPatientSymp.Style.Add("display", "none");
        }

        /// <summary>
        /// Updates the GridView to show records based on a given SQL query
        /// </summary>
        private void ShowData(string query) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        SqlDataReader reader = cmd.ExecuteReader();
                        grdResult.DataSource = reader;
                        grdResult.DataBind();
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }

        /// <summary>
        /// Shows the GridView based on the Users table
        /// </summary>
        protected void btnUsers_Click(object sender, EventArgs e) {
            // Remember the choice after post back
            ViewState["ButtonPressed"] = "Users";

            // Shows data from the Users table
            string query = "SELECT * FROM Users";
            ShowData(query);

            // Refreshes GridView and prompts
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        /// <summary>
        /// Shows the GridView based on the Patients table
        /// </summary>
        protected void btnPatients_Click(object sender, EventArgs e) {
            // Remember the choice after post back
            ViewState["ButtonPressed"] = "Patients";

            // Shows data from the Patient table
            string query = "SELECT * FROM Patient";
            ShowData(query);

            // Refreshes GridView and prompts
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        /// <summary>
        /// Shows the GridView based on the PatientHistory table
        /// </summary>
        protected void btnPatientHistory_Click(object sender, EventArgs e) {
            // Remember the choice after post back
            ViewState["ButtonPressed"] = "PatientHistory";

            // Shows data from the Patient table
            string query = "SELECT ph.PatientId, FirstName, LastName, ph.HistoryId, Name " +
                "FROM PatientHistory ph " +
                "INNER JOIN History h " +
                "ON ph.HistoryId = h.HistoryId " +
                "INNER JOIN Patient p " +
                "ON ph.PatientId = p.PatientId";
            ShowData(query);

            // Refreshes GridView and prompts
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        /// <summary>
        /// Shows the GridView based on the PatientSymptom table
        /// </summary>
        protected void btnPatientSymptom_Click(object sender, EventArgs e) {
            // Remember the choice after post back
            ViewState["ButtonPressed"] = "PatientSymptoms";

            // Shows data from the PatientSymptom table
            string query = "SELECT ps.PatientId, FirstName, LastName, ps.SymptomId, Name  " +
                "FROM PatientSymptom ps " +
                "INNER JOIN Symptom s " +
                "ON ps.SymptomId = s.SymptomId " +
                "INNER JOIN Patient p " +
                "ON ps.PatientId = p.PatientId";
            ShowData(query);

            // Refreshes GridView and prompts
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        /// <summary>
        /// Updates the textboxes, checkboxes, and dropdowns when the user selects on a specific record on the Grid View
        /// </summary>
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        SqlDataReader reader = null;

                        // Update the input form based on the User information
                        if (ViewState["ButtonPressed"].ToString() == "Users") {
                            divUsers.Style.Add("display", "block");

                            string email = grdResult.SelectedRow.Cells[1].Text;
                            cmd.CommandText = "SELECT * FROM Users WHERE Email=@Email";
                            cmd.Parameters.AddWithValue("@Email", email);

                            reader = cmd.ExecuteReader();
                            while (reader.Read()) {
                                tbEmail.Text = reader[0].ToString();
                                tbPassword.Text = reader[1].ToString();
                                if (reader[2].ToString() == "True") {
                                    cbIsAdmin.Checked = true;
                                } else {
                                    cbIsAdmin.Checked = false;
                                }
                            }

                        // Update the input form based on the Patient information
                        } else if (ViewState["ButtonPressed"].ToString() == "Patients") {
                            divPatients.Style.Add("display", "block");

                            string patientId = grdResult.SelectedRow.Cells[1].Text;
                            cmd.CommandText = "SELECT * FROM Patient WHERE PatientId=@PatientId";
                            cmd.Parameters.AddWithValue("@PatientId", patientId);

                            reader = cmd.ExecuteReader();
                            while (reader.Read()) {
                                tbFirstName.Text = reader[1].ToString();
                                tbLastName.Text = reader[2].ToString();
                                if (reader[3].ToString() == "M") {
                                    rdbSex.Items[0].Selected = true;
                                } else {
                                    rdbSex.Items[1].Selected = true;
                                }

                                tbDob.Text = reader[4].ToString().Split(' ')[0];
                                tbAddress.Text = reader[5].ToString();
                                tbCity.Text = reader[6].ToString();
                                
                                for (int i = 0; i < ddlProvince.Items.Count; i++) {
                                    if (ddlProvince.Items[i].Value == reader[7].ToString()) {
                                        ddlProvince.SelectedIndex = i;
                                    }
                                }

                                tbPostalCode.Text = reader[8].ToString();
                                tbPhoneNum.Text = reader[9].ToString();
                            }

                        // Update the input form based on the PatientHistory information
                        } else if (ViewState["ButtonPressed"].ToString() == "PatientHistory") {
                            divPatientHis.Style.Add("display", "block");

                            string patientId = grdResult.SelectedRow.Cells[1].Text;
                            string historyId = grdResult.SelectedRow.Cells[4].Text;
                            cmd.CommandText = "SELECT FirstName, LastName, Name " +
                                              "FROM PatientHistory ph " +
                                              "INNER JOIN History h " +
                                              "ON ph.HistoryId = h.HistoryId " +
                                              "INNER JOIN Patient p " +
                                              "ON ph.PatientId = p.PatientId " +
                                              "WHERE ph.PatientId=@PatientId " +
                                              "AND ph.HistoryId=@HistoryId";
                            cmd.Parameters.AddWithValue("@PatientId", patientId);
                            cmd.Parameters.AddWithValue("@HistoryId", historyId);

                            reader = cmd.ExecuteReader();
                            while (reader.Read()) {
                                tbFullNameHis.Text = reader[0].ToString() + " " + reader[1].ToString();
                                for (int i = 0; i < ddlHistory.Items.Count; i++) {
                                    if (ddlHistory.Items[i].Text == reader[2].ToString()) {
                                        ddlHistory.SelectedIndex = i;
                                    }
                                }
                            }

                        // Update the input form based on the PatientSymptoms information
                        } else if (ViewState["ButtonPressed"].ToString() == "PatientSymptoms") {
                            divPatientSymp.Style.Add("display", "block");

                            string patientId = grdResult.SelectedRow.Cells[1].Text;
                            string symptomId = grdResult.SelectedRow.Cells[4].Text;
                            cmd.CommandText = "SELECT FirstName, LastName, Name  " +
                                              "FROM PatientSymptom ps " +
                                              "INNER JOIN Symptom s " +
                                              "ON ps.SymptomId = s.SymptomId " +
                                              "INNER JOIN Patient p " +
                                              "ON ps.PatientId = p.PatientId " +
                                              "WHERE ps.PatientId=@PatientId " +
                                              "AND ps.SymptomId=@SymptomId";
                            cmd.Parameters.AddWithValue("@PatientId", patientId);
                            cmd.Parameters.AddWithValue("@SymptomId", symptomId);

                            reader = cmd.ExecuteReader();
                            while (reader.Read()) {
                                tbFullNameSymp.Text = reader[0].ToString() + " " + reader[1].ToString();
                                for (int i = 0; i < ddlSymptom.Items.Count; i++) {
                                    if (ddlSymptom.Items[i].Text == reader[2].ToString()) {
                                        ddlSymptom.SelectedIndex = i;
                                    }
                                }
                            }
                        }

                        reader.Close();
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database: " + ex.Message + "');</script>");
                }
            }
        }

        /// <summary>
        /// Allows the user to update a certain record
        /// </summary>
        private void UpdateRecord(string primaryKey, string tableName) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;

                        string query = "";

                        // Update the Patient table with a given PatientId
                        if (tableName == "Patient") {                            
                            cmd.CommandText = "UPDATE Patient " +
                            "SET FirstName=@FirstName, LastName=@LastName, " +
                            "Sex=@Sex, Birthday=@Birthday, " +
                            "Address=@Address, City=@City, " +
                            "Province=@Province, PostalCode=@PostalCode, " +
                            "PhoneNum=@PhoneNum " +
                            "WHERE PatientId=@PatientId";

                            cmd.Parameters.AddWithValue("@FirstName", tbFirstName.Text);
                            cmd.Parameters.AddWithValue("@LastName", tbLastName.Text);
                            cmd.Parameters.AddWithValue("@Sex", rdbSex.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@Birthday", tbDob.Text);
                            cmd.Parameters.AddWithValue("@Address", tbAddress.Text);
                            cmd.Parameters.AddWithValue("@City", tbCity.Text);
                            cmd.Parameters.AddWithValue("@Province", ddlProvince.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@PostalCode", tbPostalCode.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@PhoneNum", tbPhoneNum.Text);
                            cmd.Parameters.AddWithValue("@PatientId", primaryKey);

                            query = "SELECT * FROM Patient";

                        // Update the Users table with a given Email
                        } else if (tableName == "Users") {
                            cmd.CommandText = "UPDATE Users " +
                                "SET Password=@Password, IsAdmin=@IsAdmin " +
                                "WHERE Email=@Email";

                            cmd.Parameters.AddWithValue("@Password", tbPassword.Text);
                            if (cbIsAdmin.Checked) {
                                cmd.Parameters.AddWithValue("@IsAdmin", "True");
                            } else {
                                cmd.Parameters.AddWithValue("@IsAdmin", "False");
                            }
                            cmd.Parameters.AddWithValue("@Email", primaryKey);

                            query = "SELECT * FROM Users";

                        // Update the PatientHistory table with a given PatientId & HistoryId
                        } else if (tableName == "PatientHistory") {
                            string patientId = primaryKey.Split(',')[0];
                            string historyId = primaryKey.Split(',')[1];

                            cmd.CommandText = "UPDATE PatientHistory " +
                                "SET HistoryId=@NewHistoryId " +
                                "WHERE PatientId=@PatientId " +
                                "AND HistoryId=@HistoryId";

                            cmd.Parameters.AddWithValue("@NewHistoryId", ddlHistory.SelectedValue);
                            cmd.Parameters.AddWithValue("@HistoryId", historyId);
                            cmd.Parameters.AddWithValue("@PatientId", patientId);

                            query = "SELECT ph.PatientId, FirstName, LastName, ph.HistoryId, Name " +
                                    "FROM PatientHistory ph " +
                                    "INNER JOIN History h " +
                                    "ON ph.HistoryId = h.HistoryId " +
                                    "INNER JOIN Patient p " +
                                    "ON ph.PatientId = p.PatientId";

                        // Update the PatientSymptom table with a given PatientId & SymptomId
                        } else if (tableName == "PatientSymptom") {
                            string patientId = primaryKey.Split(',')[0];
                            string symptomId = primaryKey.Split(',')[1];

                            cmd.CommandText = "UPDATE PatientSymptom " +
                                "SET SymptomId=@NewSymptomId " +
                                "WHERE PatientId=@PatientId " +
                                "AND SymptomId=@SymptomId";

                            cmd.Parameters.AddWithValue("@NewSymptomId", ddlSymptom.SelectedValue);
                            cmd.Parameters.AddWithValue("@SymptomId", symptomId);
                            cmd.Parameters.AddWithValue("@PatientId", patientId);

                            query = "SELECT ps.PatientId, FirstName, LastName, ps.SymptomId, Name  " +
                                    "FROM PatientSymptom ps " +
                                    "INNER JOIN Symptom s " +
                                    "ON ps.SymptomId = s.SymptomId " +
                                    "INNER JOIN Patient p " +
                                    "ON ps.PatientId = p.PatientId";
                        }

                        cmd.ExecuteNonQuery();

                        // Updates the Grid View with the updated record
                        ShowData(query);
                        HideDivs();
                        grdResult.SelectedIndex = -1;
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database: "+ex.Message+"');</script>");
                }
            }
        }

        /// <summary>
        /// Update Patient record
        /// </summary>
        protected void btnUpdatePatient_Click(object sender, EventArgs e) {
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbDob.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbPostalCode.Text == "" || tbPhoneNum.Text == "") {
                Response.Write("<script>alert('Please enter all fields!');</script>");
                grdResult.SelectedIndex = -1;
            } else {
                string patientId = grdResult.SelectedRow.Cells[1].Text;
                UpdateRecord(patientId, "Patient");
            }
        }

        /// <summary>
        /// Update Users record
        /// </summary>
        protected void btnUpdateUser_Click(object sender, EventArgs e) {
            if (tbPassword.Text == "") {
                Response.Write("<script>alert('Password must not be empty!');</script>");
                grdResult.SelectedIndex = -1;
            } else {
                string email = grdResult.SelectedRow.Cells[1].Text;
                UpdateRecord(email, "Users");
            }
        }

        /// <summary>
        /// Update PatientHistory record
        /// </summary>
        protected void btnUpdateHistory_Click(object sender, EventArgs e) {
            int count = 0;
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string historyId = grdResult.SelectedRow.Cells[4].Text;
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";

            // Checks first if another record with the same PatientId & HistoryId combination already exists (i.e. count == 0)
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT COUNT(*) FROM PatientHistory WHERE PatientId=@PatientId AND HistoryId=@HistoryId";
                        cmd.Parameters.AddWithValue("@PatientId", patientId);
                        cmd.Parameters.AddWithValue("@HistoryId", ddlHistory.SelectedValue);
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }

            if (count == 0) {
                // Update record if no record exists
                UpdateRecord(patientId + "," + historyId, "PatientHistory");
            } else {
                // Record already exists
                Response.Write("<script>alert('Record already exists!');</script>");
                grdResult.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Update PatientSymptom record
        /// </summary>
        protected void btnUpdateSymptom_Click(object sender, EventArgs e) {
            int count = 0;
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string symptomId = grdResult.SelectedRow.Cells[4].Text;
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";

            // Checks first if another record with the same PatientId & SymptomId combination already exists (i.e. count == 0)
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT COUNT(*) FROM PatientSymptom WHERE PatientId=@PatientId AND SymptomId=@SymptomId";
                        cmd.Parameters.AddWithValue("@PatientId", patientId);
                        cmd.Parameters.AddWithValue("@SymptomId", ddlSymptom.SelectedValue);
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                        
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
            if (count == 0) {
                // Update record if no record exists
                UpdateRecord(patientId + "," + symptomId, "PatientSymptom");
            } else {
                // Record already exists
                Response.Write("<script>alert('Record already exists!');</script>");
                grdResult.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Allows the user to delete a certain record 
        /// </summary>
        private void DeleteRecord(string primaryKey, string tableName) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        string query = "";

                        // Delete from Patient table
                        if (tableName == "Patient") {                            
                            cmd.CommandText = "DELETE FROM PatientHistory WHERE PatientId=@PatientId;" +
                                              "DELETE FROM PatientSymptom WHERE PatientId=@PatientId;" +
                                              "DELETE FROM Patient WHERE PatientId=@PatientId;";
                            cmd.Parameters.AddWithValue("@PatientId", primaryKey);

                            query = "SELECT * FROM Patient";

                        // Delete from Users table
                        } else if (tableName == "Users") {
                            
                            cmd.CommandText = "DELETE FROM Users " +
                            "WHERE Email=@Email";
                            cmd.Parameters.AddWithValue("@Email", primaryKey);

                            query = "SELECT * FROM Users";

                        // Delete from PatientHistory table
                        } else if (tableName == "PatientHistory") {
                            
                            string patientId = primaryKey.Split(',')[0];
                            string historyId = primaryKey.Split(',')[1];

                            cmd.CommandText = "DELETE FROM PatientHistory " +
                                "WHERE PatientId=@PatientId " +
                                "AND HistoryId=@HistoryId";

                            cmd.Parameters.AddWithValue("@HistoryId", historyId);
                            cmd.Parameters.AddWithValue("@PatientId", patientId);

                            query = "SELECT ph.PatientId, FirstName, LastName, ph.HistoryId, Name " +
                                    "FROM PatientHistory ph " +
                                    "INNER JOIN History h " +
                                    "ON ph.HistoryId = h.HistoryId " +
                                    "INNER JOIN Patient p " +
                                    "ON ph.PatientId = p.PatientId";

                        // Delete from PatientSymptom table
                        } else if (tableName == "PatientSymptom") {
                            
                            string patientId = primaryKey.Split(',')[0];
                            string symptomId = primaryKey.Split(',')[1];

                            cmd.CommandText = "DELETE FROM PatientSymptom " +
                                "WHERE PatientId=@PatientId " +
                                "AND SymptomId=@SymptomId";
                            
                            cmd.Parameters.AddWithValue("@SymptomId", symptomId);
                            cmd.Parameters.AddWithValue("@PatientId", patientId);

                            query = "SELECT ps.PatientId, FirstName, LastName, ps.SymptomId, Name  " +
                                    "FROM PatientSymptom ps " +
                                    "INNER JOIN Symptom s " +
                                    "ON ps.SymptomId = s.SymptomId " +
                                    "INNER JOIN Patient p " +
                                    "ON ps.PatientId = p.PatientId";
                        }
                        cmd.ExecuteNonQuery();

                        // Updates the Grid View without the deleted record
                        ShowData(query);
                        HideDivs();
                        grdResult.SelectedIndex = -1;
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }

        /// <summary>
        /// Delete Patient record
        /// </summary>
        protected void btnDeletePatient_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            DeleteRecord(patientId, "Patient");
        }

        /// <summary>
        /// Delete Users record
        /// </summary>
        protected void btnDeleteUser_Click(object sender, EventArgs e) {
            string email = grdResult.SelectedRow.Cells[1].Text;
            DeleteRecord(email, "Users");
        }

        /// <summary>
        /// Delete PatientHistory record
        /// </summary>
        protected void btnDeleteHistory_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string historyId = grdResult.SelectedRow.Cells[4].Text;
            DeleteRecord(patientId + "," + historyId, "PatientHistory");
        }

        /// <summary>
        /// Delete PatientSymptom record
        /// </summary>
        protected void btnDeleteSymptom_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string symptomId = grdResult.SelectedRow.Cells[4].Text;
            DeleteRecord(patientId + "," + symptomId, "PatientSymptom");
        }
    }
}
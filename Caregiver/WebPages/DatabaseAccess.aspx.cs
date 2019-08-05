using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Caregiver.Web_Pages {
    public partial class DatabaseAccess : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}




                //if (!(bool)Session["IsAdmin"]) {
                //    Server.Transfer("Home.aspx");
                //}                
            }
            HideDivs();
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        private void HideDivs() {
            divPatients.Style.Add("display", "none");
            divUsers.Style.Add("display", "none");
            divPatientHis.Style.Add("display", "none");
            divPatientSymp.Style.Add("display", "none");
        }

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
        protected void btnUsers_Click(object sender, EventArgs e) {
            ViewState["ButtonPressed"] = "Users";
            string query = "SELECT * FROM Users";
            ShowData(query);
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        protected void btnPatients_Click(object sender, EventArgs e) {
            ViewState["ButtonPressed"] = "Patients";
            string query = "SELECT * FROM Patient";
            ShowData(query);
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        protected void btnPatientHistory_Click(object sender, EventArgs e) {
            ViewState["ButtonPressed"] = "PatientHistory";
            string query = "SELECT ph.PatientId, FirstName, LastName, ph.HistoryId, Name " +
                "FROM PatientHistory ph " +
                "INNER JOIN History h " +
                "ON ph.HistoryId = h.HistoryId " +
                "INNER JOIN Patient p " +
                "ON ph.PatientId = p.PatientId";
            ShowData(query);
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        protected void btnPatientSymptom_Click(object sender, EventArgs e) {
            ViewState["ButtonPressed"] = "PatientSymptoms";
            string query = "SELECT ps.PatientId, FirstName, LastName, ps.SymptomId, Name  " +
                "FROM PatientSymptom ps " +
                "INNER JOIN Symptom s " +
                "ON ps.SymptomId = s.SymptomId " +
                "INNER JOIN Patient p " +
                "ON ps.PatientId = p.PatientId";
            ShowData(query);
            grdResult.SelectedIndex = -1;
            HideDivs();
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        SqlDataReader reader = null;

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










        private void UpdateRecord(string primaryKey, string tableName) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;

                        string query = "";
                        if (tableName == "Patient") {
                            // Update the Patient table
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
                        } else if (tableName == "Users") {
                            // Update the Users table
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

                        } else if (tableName == "PatientHistory") {
                            // Update the PatientHistory table
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

                        } else if (tableName == "PatientSymptom") {
                            // Update the PatientSymptom table
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

                        ShowData(query);
                        HideDivs();
                        grdResult.SelectedIndex = -1;
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database: "+ex.Message+"');</script>");
                }
            }
        }

        protected void btnUpdatePatient_Click(object sender, EventArgs e) {
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbDob.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbPostalCode.Text == "" || tbPhoneNum.Text == "") {
                Response.Write("<script>alert('Please enter all fields!');</script>");
                grdResult.SelectedIndex = -1;
            } else {
                string patientId = grdResult.SelectedRow.Cells[1].Text;
                UpdateRecord(patientId, "Patient");
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e) {
            if (tbPassword.Text == "") {
                Response.Write("<script>alert('Password must not be empty!');</script>");
                grdResult.SelectedIndex = -1;
            } else {
                string email = grdResult.SelectedRow.Cells[1].Text;
                UpdateRecord(email, "Users");
            }
        }

        protected void btnUpdateHistory_Click(object sender, EventArgs e) {
            int count = 0;
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string historyId = grdResult.SelectedRow.Cells[4].Text;
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";

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
                // update record
                UpdateRecord(patientId + "," + historyId, "PatientHistory");
            } else {
                // record already exists
                Response.Write("<script>alert('Record already exists!');</script>");
                grdResult.SelectedIndex = -1;
            }
        }

        protected void btnUpdateSymptom_Click(object sender, EventArgs e) {
            int count = 0;
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string symptomId = grdResult.SelectedRow.Cells[4].Text;
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";

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
                // update record
                UpdateRecord(patientId + "," + symptomId, "PatientSymptom");
            } else {
                // record already exists
                Response.Write("<script>alert('Record already exists!');</script>");
                grdResult.SelectedIndex = -1;
            }
        }










        private void DeleteRecord(string primaryKey, string tableName) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = conString;
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        string query = "";

                        if (tableName == "Patient") {
                            // Delete from Patient table
                            cmd.CommandText = "DELETE FROM PatientHistory WHERE PatientId=@PatientId;" +
                                              "DELETE FROM PatientSymptom WHERE PatientId=@PatientId;" +
                                              "DELETE FROM Patient WHERE PatientId=@PatientId;";
                            cmd.Parameters.AddWithValue("@PatientId", primaryKey);

                            query = "SELECT * FROM Patient";

                        } else if (tableName == "Users") {
                            // Delete from Users table
                            cmd.CommandText = "DELETE FROM Users " +
                            "WHERE Email=@Email";
                            cmd.Parameters.AddWithValue("@Email", primaryKey);

                            query = "SELECT * FROM Users";

                        } else if (tableName == "PatientHistory") {
                            // Delete from PatientHistory table
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

                        } else if (tableName == "PatientSymptom") {
                            // Delete from PatientSymptom table
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

                        ShowData(query);
                        HideDivs();
                        grdResult.SelectedIndex = -1;
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }

        protected void btnDeletePatient_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            DeleteRecord(patientId, "Patient");
        }


        protected void btnDeleteUser_Click(object sender, EventArgs e) {
            string email = grdResult.SelectedRow.Cells[1].Text;
            DeleteRecord(email, "Users");
        }



        protected void btnDeleteHistory_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string historyId = grdResult.SelectedRow.Cells[4].Text;
            DeleteRecord(patientId + "," + historyId, "PatientHistory");
        }



        protected void btnDeleteSymptom_Click(object sender, EventArgs e) {
            string patientId = grdResult.SelectedRow.Cells[1].Text;
            string symptomId = grdResult.SelectedRow.Cells[4].Text;
            DeleteRecord(patientId + "," + symptomId, "PatientSymptom");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Caregiver.Web_Pages {
    public partial class Patient : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }

                //Response.Write("<script>alert('"+Session["PatientId"] +"');</script>");
                Patient patient = new Patient();
                using (SqlConnection conn = new SqlConnection()) {
                    conn.ConnectionString = "server=(local);database=Caregiver;integrated security=SSPI;";
                    using(SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from Patient;";

                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        while(reader.Read()) {
                            // reader[0] = id
                            // reader[1] = first name
                            // reader[2] = last name
                            // reader[3] = sex 
                            // reader[4] = birthday
                            // reader[5] = address
                            // reader[6] = city
                            // reader[7] = province 
                            // reader[8] = postal code
                            // reader[9] = phone number
                            patient.FirstName = 
                            if ((string)reader[3] == "M") {
                                imgUser.ImageUrl = "./Images/Male.png";
                            } else {
                                imgUser.ImageUrl = "./Images/Female.png";
                            }

                            tbFirstName.Text = (string)reader[1];
                            tbLastName.Text = (string)reader[2];
                            tbSex.Text = (string)reader[3];
                            tbDob.Text = reader[4].ToString();
                            tbAddress.Text = (string)reader[5];
                            tbCity.Text = (string)reader[6];
                            
                            switch((string)reader[7]) {
                                case "ON":
                                    ddlProvince.Items[0].Selected = true;
                                    break;
                                case "AB":
                                    ddlProvince.Items[1].Selected = true;
                                    break;
                                case "BC":
                                    ddlProvince.Items[2].Selected = true;
                                    break;
                                case "MB":
                                    ddlProvince.Items[3].Selected = true;
                                    break;
                                case "NB":
                                    ddlProvince.Items[4].Selected = true;
                                    break;
                                case "NL":
                                    ddlProvince.Items[5].Selected = true;
                                    break;
                                case "NT":
                                    ddlProvince.Items[6].Selected = true;
                                    break;
                                case "NS":
                                    ddlProvince.Items[7].Selected = true;
                                    break;
                                case "NU":
                                    ddlProvince.Items[8].Selected = true;
                                    break;
                                case "PE":
                                    ddlProvince.Items[9].Selected = true;
                                    break;
                                case "QC":
                                    ddlProvince.Items[10].Selected = true;
                                    break;
                                case "SK":
                                    ddlProvince.Items[11].Selected = true;
                                    break;
                                case "YT":
                                    ddlProvince.Items[12].Selected = true;
                                    break;
                            }
                           
                            tbPostalCode.Text = (string)reader[8];
                            tbPhoneNum.Text = (string)reader[9];
                        } // end of patient table info

                        reader.Close();

                        cmd.CommandText = "SELECT h.Name FROM History h join PatientHistory " +
                            "on h.HistoryId = PatientHistory.HistoryId join Patient " +
                            "on Patient.PatientId = PatientHistory.PatientId " +
                            "where Patient.PatientId = @id";
                        cmd.Parameters.AddWithValue("@id", Session["PatientId"]);

                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.HasRows) {
                            while(rdr.Read()) {
                                switch((string)rdr[0]) {
                                    case "Heart Disease":
                                        cblHistory.Items[0].Selected = true;
                                        break;
                                    case "Smoking":
                                        cblHistory.Items[1].Selected = true;
                                        break;
                                    case "Diabetes":
                                        cblHistory.Items[2].Selected = true;
                                        break;
                                    case "High Blood Pressure":
                                        cblHistory.Items[3].Selected = true;
                                        break;
                                    case "Stroke":
                                        cblHistory.Items[4].Selected = true;
                                        break;
                                }
                            }
                        }
                        rdr.Close();

                        cmd.CommandText = "SELECT s.Name FROM Symptom s join PatientSymptom " +
                            "on s.SymptomId = PatientSymptom.SymptomId join Patient " +
                            "on Patient.PatientId = PatientSymptom.PatientId " +
                            "where Patient.PatientId = @id2; ";
                        cmd.Parameters.AddWithValue("@id2", Session["PatientId"]);

                        SqlDataReader rdr3 = cmd.ExecuteReader();
                        if (rdr3.HasRows) {
                            while (rdr3.Read()) {
                                switch ((string)rdr3[0]) {
                                    case "Chest Pain":
                                        cblSymptom.Items[0].Selected = true;
                                        break;
                                    case "Shortness of Breath":
                                        cblSymptom.Items[1].Selected = true;
                                        break;
                                    case "Numbness":
                                        cblSymptom.Items[2].Selected = true;
                                        break;
                                    case "Dizziness":
                                        cblSymptom.Items[3].Selected = true;
                                        break;
                                    case "Fever":
                                        cblSymptom.Items[4].Selected = true;
                                        break;
                                    case "Vomiting":
                                        cblSymptom.Items[5].Selected = true;
                                        break;
                                    case "Constant Urination":
                                        cblSymptom.Items[6].Selected = true;
                                        break;
                                }
                            }
                        }
                        rdr3.Close();
                    }
                }
            }
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }
    }
}
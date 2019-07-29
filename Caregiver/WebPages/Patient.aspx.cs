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

        private Classes.Patient patient = new Classes.Patient();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }

                //Response.Write("<script>alert('"+Session["PatientId"] +"');</script>");

                using (SqlConnection conn = new SqlConnection()) {
                    conn.ConnectionString = "server=(local);database=Caregiver;integrated security=SSPI;";
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from Patient;";

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read()) {
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
                            patient.FirstName = (string)reader[1];
                            patient.LastName = (string)reader[2];
                            patient.Sex = (char)reader[3];
                            patient.Dob = reader[4].ToString();
                            patient.Address = (string)reader[5];
                            patient.City = (string)reader[6];
                            patient.Province = (string)reader[7];
                            patient.PostalCode = (string)reader[8];
                            patient.PhoneNum = (string)reader[9];
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

                            switch ((string)reader[7]) {
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
                        if (rdr.HasRows) {
                            while (rdr.Read()) {
                                switch ((string)rdr[0]) {
                                    case "Heart Disease":
                                        patient.History.Add("Heart Disease");
                                        cblHistory.Items[0].Selected = true;
                                        break;
                                    case "Smoking":
                                        patient.History.Add("Smoking");
                                        cblHistory.Items[1].Selected = true;
                                        break;
                                    case "Diabetes":
                                        patient.History.Add("Diabetes");
                                        cblHistory.Items[2].Selected = true;
                                        break;
                                    case "High Blood Pressure":
                                        patient.History.Add("High Blood Pressure");
                                        cblHistory.Items[3].Selected = true;
                                        break;
                                    case "Stroke":
                                        patient.History.Add("Stroke");
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
                                        patient.Symptoms.Add("Chest Pain");
                                        cblSymptom.Items[0].Selected = true;
                                        break;
                                    case "Shortness of Breath":
                                        patient.Symptoms.Add("Shortness of Breath");
                                        cblSymptom.Items[1].Selected = true;
                                        break;
                                    case "Numbness":
                                        patient.Symptoms.Add("Numbness");
                                        cblSymptom.Items[2].Selected = true;
                                        break;
                                    case "Dizziness":
                                        patient.Symptoms.Add("Dizziness");
                                        cblSymptom.Items[3].Selected = true;
                                        break;
                                    case "Fever":
                                        patient.Symptoms.Add("Fever");
                                        cblSymptom.Items[4].Selected = true;
                                        break;
                                    case "Vomiting":
                                        patient.Symptoms.Add("Vomiting");
                                        cblSymptom.Items[5].Selected = true;
                                        break;
                                    case "Constant Urination":
                                        patient.Symptoms.Add("Constant Urination");
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

        protected void btnDiagnose_Click(object sender, EventArgs e) {
            int age = patient.CalculateAge();
            string result = "";
            int coronaryArteryDiseaseChance = 0;
            int strokeChance = 0;
            int fluChance = 0;
            //Coronary Artery Disease
            //Person criteria:
            //Female – Age >= 55
            //Male – Age >= 45
            //History criteria:
            //Heart Disease
            //Smoking
            //Diabetes
            //Symptoms criteria:
            //Chest Pain
            //Shortness of Breath
            //Numbness
            //Dizziness
            //High Blood Pressure

            if (patient.Sex == 'F' && age >= 55 || patient.Sex == 'M' && age >= 45) {
               if(patient.History.Contains("Heart Disease") && patient.History.Contains("Smoking") 
                    && patient.History.Contains("Diabetes") && patient.Symptoms.Contains("Chest Pain")
                    && patient.Symptoms.Contains("Shortness of Breath") && patient.Symptoms.Contains("Numbness")
                    && patient.Symptoms.Contains("Dizziness") && patient.Symptoms.Contains("High Blood Pressure")) {
                    result = "Coronary Artery Disease";
                }
            }

            //Stroke
            //Person criteria:
            //Men > Female
            //Females – Age >= 55
            //History criteria:
            //Smoking
            //Stroke
            //Symptoms criteria:
            //High Blood Pressure
            //Dizziness
            //Numbness
            if (patient.Sex == 'F' && age >= 55 || patient.Sex == 'M' && age > 55) {
                if(patient.History.Contains("Smoking") && patient.History.Contains("Stroke") 
                    && patient.Symptoms.Contains("High Blood Pressure") && patient.Symptoms.Contains("Dizziness")
                    && patient.Symptoms.Contains("Numbness")) {
                    result = "Stroke";
                }
            }

            //Flu(Influenza)
            //Person criteria:
            //Age <= 2 or >= 65
            //History criteria:
            //Symptoms criteria:
            //Shortness of Breath
            //Dizziness
            //Fever
            //Vomiting
            if (age <= 2 || age >= 65) {
                if(patient.Symptoms.Contains("Shortness of Breath") && patient.Symptoms.Contains("Dizziness")
                    && patient.Symptoms.Contains("Fever") && patient.Symptoms.Contains("Vomiting")) {
                    result = "Flu(Influenza)";
                }
            }

            //Kidney Disease
            //Person criteria:
            //Age >= 60
            //History criteria:
            //Symptoms criteria:
            //Vomiting
            //Constant urination
            //Shortness of Breath
            if (age >= 60) {
                if(patient.Symptoms.Contains("Vomiting") && patient.Symptoms.Contains("Constant Urination")
                    && patient.Symptoms.Contains("Shortness of Breath")) {
                    result = "Kidney Disease";
                }
            }

            lblDiagnosis.Text = result;

        }

        protected void tbEdit_Click(object sender, EventArgs e) {

        }
    }
}
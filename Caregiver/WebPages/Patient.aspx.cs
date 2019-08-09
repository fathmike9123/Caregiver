using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

/// <author>Ryan Haire</author>
/// <summary>
/// This class is for patient information editing and diagnosis of disease
/// </summary>

namespace Caregiver.Web_Pages {
    public partial class Patient : System.Web.UI.Page {

        private Classes.Patient patient;

        /// <summary>
        /// On page load, the form values are filled with the patients data from the database
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            SetEnabled(false);
            tbEdit.Style.Add("display", "inline");
            tbSave.Style.Add("display", "none");


            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}

                // initialize patient object
                patient = new Classes.Patient();

                using (SqlConnection conn = new SqlConnection()) {
                    conn.ConnectionString = "server=(local);database=Caregiver;integrated security=SSPI;";
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM Patient WHERE PatientId=@id;";
                        cmd.Parameters.AddWithValue("@id", Session["PatientId"]);

                        SqlDataReader reader = cmd.ExecuteReader();

                        // Get all of the patient's information
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
                            patient.Sex = Convert.ToChar(reader[3]);
                            patient.Dob = reader[4].ToString();
                            patient.Address = (string)reader[5];
                            patient.City = (string)reader[6];
                            patient.Province = (string)reader[7];
                            patient.PostalCode = (string)reader[8];
                            patient.PhoneNum = (string)reader[9];
                            
                            tbFirstName.Text = (string)reader[1];
                            tbLastName.Text = (string)reader[2];
                            if ((string)reader[3] == "M") {
                                rdbSex.SelectedIndex = 0;
                                imgUser.ImageUrl = "/Images/Male.png";
                            } else {
                                rdbSex.SelectedIndex = 1;
                                imgUser.ImageUrl = "/Images/Female.png";
                            }
                            tbDob.Text = reader[4].ToString().Split(' ')[0];
                            tbAddress.Text = (string)reader[5];
                            tbCity.Text = (string)reader[6];

                            for (int i = 0; i < ddlProvince.Items.Count; i++) {
                                if (ddlProvince.Items[i].Value == reader[7].ToString()) {
                                    ddlProvince.SelectedIndex = i;
                                }
                            }

                            tbPostalCode.Text = (string)reader[8];
                            tbPhoneNum.Text = (string)reader[9];
                        } // end of patient table info

                        reader.Close();

                        // Get all of the patient's history
                        cmd.CommandText = "SELECT h.Name FROM History h JOIN PatientHistory " +
                            "ON h.HistoryId = PatientHistory.HistoryId JOIN Patient " +
                            "ON Patient.PatientId = PatientHistory.PatientId " +
                            "WHERE Patient.PatientId = @id";
                        

                        reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                string history = reader[0].ToString();
                                for (int i = 0; i < cblHistory.Items.Count; i++) {
                                    if (cblHistory.Items[i].Text == history) {
                                        patient.History.Add(history);
                                        cblHistory.Items[i].Selected = true;
                                    }
                                }
                            }
                        }
                        reader.Close();

                        // Get all of the patient's symptoms
                        cmd.CommandText = "SELECT s.Name FROM Symptom s JOIN PatientSymptom " +
                            "ON s.SymptomId = PatientSymptom.SymptomId JOIN Patient " +
                            "ON Patient.PatientId = PatientSymptom.PatientId " +
                            "WHERE Patient.PatientId = @id; ";

                        reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                string symptom = reader[0].ToString();
                                for (int i = 0; i < cblSymptom.Items.Count; i++) {
                                    if (cblSymptom.Items[i].Text == symptom) {
                                        patient.Symptoms.Add(symptom);
                                        cblSymptom.Items[i].Selected = true;
                                    }
                                }
                            }
                        }
                        reader.Close();
                    }
                }

                ViewState["Patient"] = this.patient;

            } else {
                this.patient = (Classes.Patient) ViewState["Patient"];
            }
        }

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void SetEnabled(bool value) {
            tbFirstName.Enabled = value;
            tbLastName.Enabled = value;
            rdbSex.Enabled = value;
            tbDob.Enabled = value;
            cblHistory.Enabled = value;
            tbAddress.Enabled = value;
            tbCity.Enabled = value;
            ddlProvince.Enabled = value;
            tbPostalCode.Enabled = value;
            tbPhoneNum.Enabled = value;
            cblSymptom.Enabled = value;
        }

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        /// <summary>
        /// This function is for calculating the diagnosis of the patient by checking what symptoms
        /// and history they have compared to the disease that contains the respective symptoms and diseases
        /// Then outputs the diagnosis
        /// </summary>
        protected void btn_Diagnose(object sender, EventArgs e) {
            string result = "";

            int coronaryArteryDiseaseChance = patient.CalculateCoronaryArteryChance();
            int strokeChance = patient.CalculateStrokeChance();
            int fluChance = patient.CalculateFluChance();
            int kidneyDiseaseChance = patient.CalculateKidneyDiseaseChance();
            
            // check if all chances are equal
            if (coronaryArteryDiseaseChance == strokeChance && coronaryArteryDiseaseChance == fluChance && coronaryArteryDiseaseChance == kidneyDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            } // check if coronaryArteryDiseaseChance is greater than all the others 

            // if one chance is greater than the others
            if (coronaryArteryDiseaseChance > strokeChance && coronaryArteryDiseaseChance > fluChance && coronaryArteryDiseaseChance > kidneyDiseaseChance) {
                result = "Probable chance of Coronary Artery Disease";
            }// check if strokeChance is greater than all the others  
            else if (strokeChance > coronaryArteryDiseaseChance && strokeChance > fluChance && strokeChance > kidneyDiseaseChance) {
                result = "Probable chance of Stroke";
            }// check if fluChance is greater than all the others  
            else if (fluChance > coronaryArteryDiseaseChance && fluChance > strokeChance && fluChance > kidneyDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            } // check if kidneyDiseaseChance is greater than all the others 
            else if (kidneyDiseaseChance > coronaryArteryDiseaseChance && kidneyDiseaseChance > fluChance && kidneyDiseaseChance > strokeChance) {
                result = "Probable chance of Kidney Disease";
            } 
            
            // check if any 2 are equal and greater than the other 2 -- if
            if (coronaryArteryDiseaseChance == strokeChance && strokeChance > fluChance && strokeChance > kidneyDiseaseChance) {
                result = "Probable chance of Stroke";
            } else if (coronaryArteryDiseaseChance == fluChance && fluChance > strokeChance && fluChance > kidneyDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            } else if (coronaryArteryDiseaseChance == kidneyDiseaseChance && kidneyDiseaseChance > fluChance && kidneyDiseaseChance > strokeChance) {
                result = "Probable chance of Kidney Disease";
            } else if (strokeChance == fluChance && fluChance > coronaryArteryDiseaseChance && fluChance > kidneyDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            } else if (strokeChance == kidneyDiseaseChance && strokeChance > coronaryArteryDiseaseChance && strokeChance > fluChance) {
                result = "Probable chance of stroke";
            } else if (fluChance == kidneyDiseaseChance && fluChance > coronaryArteryDiseaseChance && fluChance > strokeChance) {
                result = "Probable chance of Flu(Influenza)";
            } 
            
            // check if any 3 are equal && greater than the other one -- if 
            if (coronaryArteryDiseaseChance == strokeChance && coronaryArteryDiseaseChance == fluChance && fluChance > kidneyDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            } else if (coronaryArteryDiseaseChance == strokeChance && coronaryArteryDiseaseChance == kidneyDiseaseChance && strokeChance > fluChance) {
                result = "Probable chance of Stroke";
            } else if (coronaryArteryDiseaseChance == fluChance && coronaryArteryDiseaseChance == kidneyDiseaseChance && fluChance > strokeChance) {
                result = "Probable chance of Flu(Influenza)";
            } else if (strokeChance == fluChance && strokeChance == kidneyDiseaseChance && strokeChance > coronaryArteryDiseaseChance) {
                result = "Probable chance of Flu(Influenza)";
            }
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

            //Flu(Influenza)
            //Person criteria:
            //Age <= 2 or >= 65
            //History criteria:
            //Symptoms criteria:
            //Shortness of Breath
            //Dizziness
            //Fever
            //Vomiting

            //Kidney Disease
            //Person criteria:
            //Age >= 60
            //History criteria:
            //Symptoms criteria:
            //Vomiting
            //Constant urination
            //Shortness of Breath

            if (coronaryArteryDiseaseChance == 0 && strokeChance == 0 && fluChance == 0 && kidneyDiseaseChance == 0) {
                result = "No diagnosis.";
            }

            lbl1.Text = "coronaryArteryDiseaseChance = " + coronaryArteryDiseaseChance.ToString();
            lbl2.Text = "strokeChance = " + strokeChance.ToString();
            lbl3.Text = "fluChance = " + fluChance.ToString();
            lbl4.Text = "kidneyDiseaseChance = " + kidneyDiseaseChance.ToString();

            lblDiagnosis.Text = result;
        }

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        protected void tbEdit_Click(object sender, EventArgs e) {
            SetEnabled(true);
            tbEdit.Style.Add("display", "none");
            tbSave.Style.Add("display", "inline");
        }

        /// <summary>
        /// This function saves the patient information that was edited into the database
        /// </summary>
        protected void tbSave_Click(object sender, EventArgs e) {
            tbEdit.Style.Add("display", "inline");
            tbSave.Style.Add("display", "none");
            
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = "server=(local);database=Caregiver;Integrated Security=SSPI";
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;

                        cmd.CommandText = "Delete from PatientHistory where PatientId=@id;"
                                        + "Delete from PatientSymptom where PatientId=@id";
                        cmd.Parameters.AddWithValue("@id", Session["PatientId"]);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Update Patient set FirstName=@FirstName,LastName=@LastName,Sex=@Sex,Birthday=@Birthday," +
                                          "Address=@Address,City=@City,Province=@Province,PostalCode=@PostalCode,PhoneNum=@PhoneNum" +
                                          " where PatientId = @id2;";
                        cmd.Parameters.AddWithValue("@FirstName", tbFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", tbLastName.Text);
                        cmd.Parameters.AddWithValue("@Sex", rdbSex.SelectedValue);
                        cmd.Parameters.AddWithValue("@Birthday", Convert.ToDateTime(tbDob.Text));
                        cmd.Parameters.AddWithValue("@Address", tbAddress.Text);
                        cmd.Parameters.AddWithValue("@City", tbCity.Text);
                        cmd.Parameters.AddWithValue("@Province", ddlProvince.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@PostalCode", tbPostalCode.Text.ToUpper());
                        cmd.Parameters.AddWithValue("@PhoneNum", tbPhoneNum.Text);
                        cmd.Parameters.AddWithValue("@id2", Session["PatientId"]);
                        cmd.ExecuteNonQuery();

                        List<string> history = new List<string>();
                        foreach (ListItem item in cblHistory.Items) {
                            if (item.Selected) {
                                history.Add(item.Value);
                            }
                        }

                        List<string> symptoms = new List<string>();
                        foreach (ListItem item in cblSymptom.Items) {
                            if (item.Selected) {
                                symptoms.Add(item.Value);
                            }
                        }

                        foreach (string item in history) {
                            cmd.CommandText = "INSERT INTO PatientHistory VALUES(@PatientID,@HistoryId)";
                            cmd.Parameters.AddWithValue("@PatientId", Session["PatientId"]);
                            cmd.Parameters.AddWithValue("@HistoryId", item);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        foreach (string item in symptoms) {
                            cmd.CommandText = "INSERT INTO PatientSymptom VALUES(@PatientID,@SymptomId)";
                            cmd.Parameters.AddWithValue("@PatientId", Session["PatientId"]);
                            cmd.Parameters.AddWithValue("@SymptomId", item);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        lblUpdateResult.Text = "Update successfull!";
                        conn.Close();
                    }
                } catch (SqlException ex) {
                    lblUpdateResult.Text = ex.Message;
                } catch (Exception ex) {
                    lblUpdateResult.Text = ex.Message;
                }

            }
        }
    }
}
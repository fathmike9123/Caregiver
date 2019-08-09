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
/// This class is for viewing a list of all the patients in the database
/// </summary>
namespace Caregiver.Web_Pages {

    /// <author>Stefano & Ryan Haire</author>
    /// <summary>
    /// On page load the list of patients are queried from the database and displayed as links to
    /// their patient information
    /// </summary>
    public partial class ViewAllPatients : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}
            }
         
            using(SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
                try {
                    using(SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from Patient;";

                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read()) {

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

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        private void btnPatient_Click(object sender, EventArgs e) {
            int patientId = Convert.ToInt32(((LinkButton)sender).ID);
            Session["PatientId"] = patientId;
            Server.Transfer("Patient.aspx");
        }

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        /// <author>Stefano</author>
        /// <summary>
        /// 
        /// </summary>
        protected void lbReports_Click(object sender, EventArgs e) {
            Server.Transfer("Reports.aspx");
        }
    }
}
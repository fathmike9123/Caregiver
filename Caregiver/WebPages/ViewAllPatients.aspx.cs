using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Caregiver.Web_Pages {
    public partial class ViewAllPatients : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }
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
                            
                            Button btn = new Button();
                            //btn.Click += new EventHandler(profile_Click);
                            btn.Style.Add("width", "130px");
                            btn.Style.Add("height", "128px");
                            btn.Style.Add("color", "white");

                            if ((string)reader[3] == "M") {
                                btn.Style.Add("background-image", "url(/Images/Male.png)");
                                //btn.CssClass = "male-bg";
                                //imgBtn.ImageUrl = "./Images/Male.png";
                            } else {
                                btn.Style.Add("background-image", "url(/Images/Female.png)");
                                //btn.CssClass = "female-bg";
                                //imgBtn.ImageUrl = "./Images/Female.png";

                            }
                            btn.Text = reader[1] + " " + reader[2];
                            btn.ID = "patient_" + reader[0];
                            btn.PostBackUrl = "Patient.aspx?patientid=" + reader[0];

                            this.form1.Controls.Add(btn);
                            PlaceHolder1.Controls.Add(btn);
                        }
                        
                    }
                } catch (SqlException ex) {

                }
            }
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        protected void lbReports_Click(object sender, EventArgs e) {
            Server.Transfer("Reports.aspx");
        }
    }
}
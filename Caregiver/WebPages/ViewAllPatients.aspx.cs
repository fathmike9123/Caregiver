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

                            Panel divOuter = new Panel();
                            divOuter.CssClass = "card shadow-sm p - 3 mb - 5 bg - white rounded";

                            LinkButton btn = new LinkButton();
                            divOuter.Controls.Add(btn);

                            Image img = new Image();
                            btn.Controls.Add(img);
                            img.Style.Add("padding", "10%");
                            img.CssClass = "card-img-top";

                            Panel divInner = new Panel();
                            divInner.CssClass = "card-body";
                            btn.Controls.Add(divInner);

                            Label innerHtml = new Label();
                            innerHtml.Text = reader[1] + " " + reader[2];
                            innerHtml.CssClass = "card-title";
                            divInner.Controls.Add(innerHtml);

                            if ((string)reader[3] == "M") {
                                img.ImageUrl = "/Images/Male.png";
                            } else {
                                img.ImageUrl = "/Images/Female.png";
                            }
                            btn.ID = reader[0].ToString();
                            btn.Click += btnPatient_Click;

                            PlaceHolder2.Controls.Add(divOuter);
                        }
                        
                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }

        private void btnPatient_Click(object sender, EventArgs e) {
            int patientId = Convert.ToInt32(((LinkButton)sender).ID);
            Session["PatientId"] = patientId;
            Server.Transfer("Patient.aspx");
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        protected void lbReports_Click(object sender, EventArgs e) {
            Server.Transfer("Reports.aspx");
        }
    }
}
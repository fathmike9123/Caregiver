using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Caregiver.Web_Pages {
    public partial class Search : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}
                tbText.Style.Add("display", "inline");
                tbPhoneNum.Style.Add("display", "none");
                cbSymptom.Style.Add("display", "none");
            } else {
                tbText.Style.Add("display", "none");
                tbPhoneNum.Style.Add("display", "none");
                cbSymptom.Style.Add("display", "none");
            }
            
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            int selectedIndex = ddlChoice.SelectedIndex;
            if (selectedIndex == 4) {

            } else if (selectedIndex == 3){
            }
        }

        private void SearchCriteria(string append) {
            string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(conString)) {
                try {
                    using (SqlCommand cmd = new SqlCommand()) {
                        conn.Open();
                        cmd.Connection = conn;
                        string query = "SELECT * FROM Users WHERE" + append
                        cmd.CommandText = "SELECT * FROM Users";
                        SqlDataReader reader = cmd.ExecuteReader();
                        gridViewResult.DataSource = reader;
                        gridViewResult.DataBind();

                        reader.Close();


                    }
                } catch (SqlException ex) {
                    Response.Write("<script>alert('An error has occured with the database');</script>");
                }
            }
        }
        protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlChoice.SelectedIndex == 3 ) {
                tbPhoneNum.Style.Add("display", "inline");
            } else if (ddlChoice.SelectedIndex == 4) {
                cbSymptom.Style.Add("display", "block");
            } else {
                tbText.Style.Add("display", "inline");
            }
        }
    }
}
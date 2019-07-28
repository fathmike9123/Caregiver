using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Caregiver {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Session["IsRegisteredUser"] = false;
            Session["IsAdmin"] = false;
        }

        protected void lbSignIn_Click(object sender, EventArgs e) {
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            if (email == "" || password == "") {
                Response.Write("<script>alert('You must fill in all text boxes.');</script>");
            } else {
                string conString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
                using(SqlConnection conn = new SqlConnection(conString)) {
                    try {                        
                        using (SqlCommand cmd = new SqlCommand()) {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT * FROM Users";
                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read()) {
                                if (reader[0].ToString() == email && reader[1].ToString() == password) {
                                    if (reader[2].ToString() == "True") {
                                        Session["IsAdmin"] = true;
                                    } else {
                                        Session["IsAdmin"] = false;
                                    }
                                    Session["IsRegisteredUser"] = true;
                                    reader.Close();
                                    Server.Transfer("Home.aspx");
                                    break;
                                }
                            }
                            Session["IsRegisteredUser"] = false;
                            Response.Write("<script>alert('Invalid username & password.');</script>");
                            reader.Close();


                        }
                    } catch (SqlException ex) {
                        Response.Write("<script>alert('An error has occured with the database');</script>");
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            
        }
    }
}
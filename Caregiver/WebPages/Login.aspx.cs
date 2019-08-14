using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <author>Ryan Haire</author>
/// <summary>
/// Code Behind File for the Login.aspx page
/// </summary>
namespace Caregiver {
    public partial class Login : System.Web.UI.Page {

        /// <summary>
        /// On page load, revoke credentials
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            Session["IsRegisteredUser"] = false;
            Session["IsAdmin"] = false;
        }

        /// <summary>
        /// Sign in to the system, if the email + password is valid
        /// </summary>
        protected void lbSignIn_Click(object sender, EventArgs e) {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            
            if (email == "" || password == "") {
                // Both input fields must NOT be empty
                warningMessage.Style.Add("display", "inline");
                warningMessage.InnerHtml = "You must fill in all text boxes!";
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

                                    // Sets admin if record states it
                                    if (reader[2].ToString() == "True") {
                                        Session["IsAdmin"] = true;
                                    } else {
                                        Session["IsAdmin"] = false;
                                    }
                                    Session["IsRegisteredUser"] = true;
                                    reader.Close();

                                    // Moves to Home.aspx page
                                    Server.Transfer("Home.aspx");
                                    break;
                                }
                            }

                            // Displays error message when account is NOT found
                            Session["IsRegisteredUser"] = false;
                            warningMessage.Style.Add("display", "inline");
                            warningMessage.InnerHtml = "Invalid email address and password!";
                            reader.Close();
                        }
                    } catch (SqlException ex) {
                        Response.Write("<script>alert('An error has occured with the database');</script>");
                    }
                }
            }
            
        }
    }
}
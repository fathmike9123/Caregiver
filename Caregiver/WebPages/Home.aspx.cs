using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <author>Ryan Haire</author>
/// <summary>
/// Code Behind File for Home.aspx
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class Home : System.Web.UI.Page {

        /// <summary>
        /// On page load, transfer the user back to Login.aspx if they are not registered.
        /// Also, only admins can view the "View Database" button
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }

                // Allow only admins to see the "View Database" button
                if ((bool)Session["IsAdmin"]) {
                    databaseDiv.Visible = true;
                    lbDatabase.Visible = true;
                } else {
                    databaseDiv.Visible = false;
                    lbDatabase.Visible = false;
                }
            }            
        }

        /// <summary>
        /// Goes to ViewAllPatients.aspx
        /// </summary>
        protected void lbViewAllPatients_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        /// <summary>
        /// Goes to CreatePatient.aspx
        /// </summary>
        protected void lbCreateNew_Click(object sender, EventArgs e) {
            Server.Transfer("CreatePatient.aspx");
        }

        /// <summary>
        /// Goes to Search.aspx
        /// </summary>
        protected void lbSearch_Click(object sender, EventArgs e) {
            Server.Transfer("Search.aspx");
        }

        /// <summary>
        /// Goes to ViewAllPatients.aspx
        /// </summary>
        protected void lbDatabase_Click(object sender, EventArgs e) {
            Server.Transfer("DatabaseAccess.aspx");
        }

        /// <summary>
        /// Returns to Login.aspx and revokes credentials
        /// </summary>
        protected void lbSignOut_Click(object sender, EventArgs e) {
            Session["IsRegisteredUser"] = false;
            Session["IsAdmin"] = false;
            Server.Transfer("Login.aspx");
        }
    }
}
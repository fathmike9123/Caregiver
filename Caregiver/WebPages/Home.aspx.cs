using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <author>Stefano Unlayao</author>
/// <summary>
/// 
/// </summary>
namespace Caregiver.Web_Pages {
    public partial class Home : System.Web.UI.Page {

        /// <summary>
        /// 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}

                //if ((bool)Session["IsAdmin"]) {
                //    databaseDiv.Visible = true;
                //    lbDatabase.Visible = true;
                //} else {
                //    databaseDiv.Visible = false;
                //    lbDatabase.Visible = false;
                //}
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        protected void lbViewAllPatients_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void lbCreateNew_Click(object sender, EventArgs e) {
            Server.Transfer("CreatePatient.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void lbSearch_Click(object sender, EventArgs e) {
            Server.Transfer("Search.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void lbDatabase_Click(object sender, EventArgs e) {
            Server.Transfer("DatabaseAccess.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void lbSignOut_Click(object sender, EventArgs e) {
            Session["IsRegisteredUser"] = false;
            Session["IsAdmin"] = false;
            Server.Transfer("Login.aspx");
        }
    }
}
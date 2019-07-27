using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Caregiver.Web_Pages {
    public partial class Home : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            // Must update so that only admin accounts can see the "View Database" button
        }

        protected void lbViewAllPatients_Click(object sender, EventArgs e) {
            Server.Transfer("ViewAllPatients.aspx");
        }

        protected void lbCreateNew_Click(object sender, EventArgs e) {
            Server.Transfer("CreatePatient.aspx");
        }

        protected void lbSearch_Click(object sender, EventArgs e) {
            Server.Transfer("Search.aspx");
        }

        protected void lbDatabase_Click(object sender, EventArgs e) {
            // Must update so that only admin accounts can access this
            Server.Transfer("DatabaseAccess.aspx");
        }

        protected void lbSignOut_Click(object sender, EventArgs e) {
            Server.Transfer("Login.aspx");
        }
    }
}
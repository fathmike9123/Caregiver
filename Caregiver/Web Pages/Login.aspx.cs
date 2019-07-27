using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Caregiver {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void lbSignIn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }
    }
}
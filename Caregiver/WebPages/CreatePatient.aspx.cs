using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Caregiver.Web_Pages {
    public partial class CreatePatient : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (!(bool)Session["IsRegisteredUser"]) {
                    Server.Transfer("Login.aspx");
                }
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }
    }
}
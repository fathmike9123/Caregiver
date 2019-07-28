using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Caregiver.Web_Pages {
    public partial class Search : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //if (!(bool)Session["IsRegisteredUser"]) {
                //    Server.Transfer("Login.aspx");
                //}
                tbFirstName.Style.Add("display", "block");
                tbLastName.Style.Add("display", "none");
                tbCity.Style.Add("display", "none");
                tbPhoneNum.Style.Add("display", "none");
                cbSymptom.Style.Add("display", "none");
            } else {
                tbFirstName.Style.Add("display", "none");
                tbLastName.Style.Add("display", "none");
                tbCity.Style.Add("display", "none");
                tbPhoneNum.Style.Add("display", "none");
                cbSymptom.Style.Add("display", "none");
            }
            
        }

        protected void lbReturn_Click(object sender, EventArgs e) {
            Server.Transfer("Home.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
        }

        protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlChoice.SelectedIndex == 0) {
                tbFirstName.Style.Add("display", "block");
            } else if (ddlChoice.SelectedIndex == 1) {
                tbLastName.Style.Add("display", "block");
            } else if (ddlChoice.SelectedIndex == 2) {
                tbCity.Style.Add("display", "block");
            } else if (ddlChoice.SelectedIndex == 3) {
                tbPhoneNum.Style.Add("display", "block");
            } else {
                cbSymptom.Style.Add("display", "block");
            }
        }
    }
}
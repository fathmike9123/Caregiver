using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Caregiver.Classes {
    public class DatabaseConnect {
        private string ConnString {
            get; set;
        }
        public DatabaseConnect() {
            ConnString = "server=(local);database=Caregiver;Integrated Security=SSPI;";
        }
        

    }
}
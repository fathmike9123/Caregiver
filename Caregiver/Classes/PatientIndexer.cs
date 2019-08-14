using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caregiver.Classes {
    public class PatientIndexer {
        private List<Patient> patientList = new List<Patient>();

        public Patient this[int id] {
            get {
                Patient p = new Patient();
                p.Id = id;
                for (int i = 0; i < patientList.Count; i++) {
                    if (patientList[i] == p) {
                        return patientList[i];
                    }
                }
                return null;
            }
        }

        public void AddPatient(Patient p) {
            patientList.Add(p);
        }
    }
}
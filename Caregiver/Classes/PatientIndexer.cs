using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Allows Patient objects to be indexed
/// </summary>
namespace Caregiver.Classes {
    public class PatientIndexer {
        /// <summary>
        /// List of patients to be indexed from
        /// </summary>
        private List<Patient> patientList = new List<Patient>();

        /// <summary>
        /// Indexer for the list (gets a patient with a specific ID)
        /// Returns null if not found
        /// </summary>
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

        /// <summary>
        /// Adds a patient into the list
        /// </summary>
        public void AddPatient(Patient p) {
            patientList.Add(p);
        }
    }
}
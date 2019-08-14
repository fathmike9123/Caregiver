using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <author>Stefano Gregor Unlayao</author>
/// <summary>
/// Represents a Patient record
/// </summary>
namespace Caregiver.Classes {

    // Allows the Patient class to be serialized + sent over the server
    [Serializable]
    public class Patient {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public string Dob { get; set; }
        public List<string> History { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNum { get; set; }
        public List<string> Symptoms { get; set; }

        /// <summary>
        /// Default constructor for the Patient
        /// </summary>
        public Patient() {
            History = new List<string>();
            Symptoms = new List<string>();
        }

        /// <summary>
        /// Constructor for the Patient
        /// </summary>
        public Patient(string fName, string lName, char sex, string dob) {
            FirstName = fName;
            LastName = lName;
            Sex = sex;
            Dob = dob;
        }

        /// <summary>
        /// Sets the location of the patient
        /// </summary>
        public void SetLocation(string address, string city, string prov, string pcode, string phone) {
            Address = address;
            City = city;
            Province = prov;
            PostalCode = pcode;
            PhoneNum = phone;
        }

        /// <author>Ryan Haire</author>
        /// <summary>
        /// Calculates the age of the patient
        /// </summary>
        public int CalculateAge() {
            DateTime dob = Convert.ToDateTime(Dob);
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear) {
                age = age - 1;
            }
            return age;
        }

        /// <author>Ryan Haire</author>
        /// <summary>
        /// Calculates the patient's chance of getting Coronary Artery Disease
        /// </summary>
        public int CalculateCoronaryArteryChance() {
            int result = 0;
            if (Sex == 'F' && CalculateAge() >= 55 || Sex == 'M' && CalculateAge() >= 45) {
                result += 10;
            }
            if (History.Contains("Heart Disease")) {
                result += 10;
            }
            if (History.Contains("Smoking")) {
                result += 10;
            }
            if (History.Contains("Diabetes")) {
                result += 10;
            }
            if (Symptoms.Contains("Chest Pain")) {
                result += 10;
            }
            if (Symptoms.Contains("Shortness of Breath")) {
                result += 10;
            }
            if (Symptoms.Contains("Dizziness")) {
                result += 10;
            }
            if (Symptoms.Contains("High Blood Pressure")) {
                result += 10;
            }
            if (Symptoms.Contains("Numbness")) {
                result += 10;
            }
            return result;
        }

        /// <author>Ryan Haire</author>
        /// <summary>
        /// Calculates the patient's chance of getting Stroke
        /// </summary>
        public int CalculateStrokeChance() {
            int result = 0;
            if (Sex == 'F' && CalculateAge() >= 55 || Sex == 'M' && CalculateAge() > 55) {
                result += 10;
            }
            if (History.Contains("Smoking")) {
                result += 10;
            }
            if (History.Contains("Stroke")) {
                result += 10;
            }
            if (Symptoms.Contains("Dizziness")) {
                result += 10;
            }
            if (Symptoms.Contains("High Blood Pressure")) {
                result += 10;
            }
            if (Symptoms.Contains("Numbness")) {
                result += 10;
            }
            return result;
        }

        /// <author>Ryan Haire</author>
        /// <summary>
        /// Calculates the patient's chance of getting Flu
        /// </summary>
        public int CalculateFluChance() {
            int result = 0;

            if (CalculateAge() <= 2 || CalculateAge() >= 65) {
                result += 10;
            }
            if (Symptoms.Contains("Shortness of Breath")) {
                result += 10;
            }
            if (Symptoms.Contains("Dizziness")) {
                result += 10;
            }

            if (Symptoms.Contains("Fever")) {
                result += 10;
            }
            if (Symptoms.Contains("Vomiting")) {
                result += 10;
            }
            return result;
        }

        /// <author>Ryan Haire</author>
        /// <summary>
        /// Calculates the patient's chance of getting Kidney Disease
        /// </summary>
        public int CalculateKidneyDiseaseChance() {
            int result = 0;
            if (CalculateAge() >= 60) {
                result += 10;
            }
            if (Symptoms.Contains("Shortness of Breath")) {
                result += 10;
            }
            if (Symptoms.Contains("Vomiting")) {
                result += 10;
            }
            if (Symptoms.Contains("Constant Urination")) {
                result += 10;
            }
            return result;
        }

        /// <summary>
        /// Checks if one object is equal to another object (based on same ID)
        /// </summary>
        public override bool Equals(object obj) {
            Patient p2 = obj as Patient;
            return this.Id == p2.Id ? true : false;
        }

        /// <summary>
        /// Checks if one object is equal to another object (based on same ID)
        /// </summary>
        public static bool operator ==(Patient p1, Patient p2) {
            if (ReferenceEquals(p1, null)) {
                return false;
            }

            if (ReferenceEquals(p2, null)) {
                return false;
            }
            return p1.Id == p2.Id ? true : false;
        }

        /// <summary>
        /// Checks if one object is NOT equal to another object (based on same ID)
        /// </summary>
        public static bool operator !=(Patient p1, Patient p2) {
            if (ReferenceEquals(p1, null)) {
                return true;
            }

            if (ReferenceEquals(p2, null)) {
                return true;
            }
            return p1.Id != p2.Id ? true : false;
        }

    }
}
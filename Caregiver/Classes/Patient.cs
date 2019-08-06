using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caregiver.Classes {
    [Serializable]
    public class Patient {
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

        public Patient() {
            History = new List<string>();
            Symptoms = new List<string>();
        }

        public Patient(string fName, string lName, char sex, string dob) {
            FirstName = fName;
            LastName = lName;
            Sex = sex;
            Dob = dob;
        }

        public void SetLocation(string address, string city, string prov, string pcode, string phone) {
            Address = address;
            City = city;
            Province = prov;
            PostalCode = pcode;
            PhoneNum = phone;
        }

        public void SetHistory(List<string> history) {
            if (history.Count == 0) {
                History = null;
            } else {
                History = history;
            }
        }

        public void SetSymptoms(List<string> symptoms) {
            if (symptoms.Count == 0) {
                Symptoms = null;
            } else {
                Symptoms = symptoms;
            }
        }

        public int CalculateAge() {
            DateTime dob = Convert.ToDateTime(Dob);
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear) {
                age = age - 1;
            }
            return age;
        }

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
    }
}
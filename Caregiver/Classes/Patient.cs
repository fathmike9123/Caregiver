using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caregiver.Classes {
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
            if(history.Count == 0) {
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
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
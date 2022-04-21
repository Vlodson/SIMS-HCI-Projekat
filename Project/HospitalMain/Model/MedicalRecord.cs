using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HospitalMain.Enums;
using System.Collections.ObjectModel;

namespace Model
{
    public class MedicalRecord : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String id;
        private String ucin;
        private String name;
        private String surname;
        private String phone_number;
        private String mail;
        private String adress;
        private Gender gender;
        private DateTime dob;

        private Doctor doctor;
        private String bloodType;
        private ObservableCollection<Report> reports;
        private ObservableCollection<String> allergens;

        public MedicalRecord ()
        {

        }
        public MedicalRecord(string id, string ucin, string name, string surname, string phone_number, string mail, string adress, Gender gender, DateTime dob, Doctor doctor, string bloodType, ObservableCollection<Report> reports, ObservableCollection<string> allergens)
        {
            this.id = id;
            this.ucin = ucin;
            this.name = name;
            this.surname = surname;
            this.phone_number = phone_number;
            this.mail = mail;
            this.adress = adress;
            this.gender = gender;
            this.dob = dob;
            this.doctor = doctor;
            this.bloodType = bloodType;
            this.reports = reports;
            this.allergens = allergens;
        }

        public String ID
        {
            get { return id; }
            set
            {
                if(id != null)
                {
                    id = value;
                    //OnPropertyChanged("ID");
                }
            }
        }

        public String UCIN
        {
            get { return ucin; }
            set
            {
                if(ucin != null)
                {
                    ucin = value;
                    OnPropertyChanged("UCIN");
                }
            }
        }
    
        public String Name
        {
            get { return name; }
            set
            {
                if(name != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
    
        public String Surname
        {
            get { return surname; }
            set
            {
                if(surname != null)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public String PhoneNumber
        {
            get { return phone_number; }
            set
            {
                if ((phone_number != value))
                {
                    phone_number = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public String Adress
        {
            get { return adress; }
            set
            {
                if (adress != value)
                {
                    adress = value;
                    OnPropertyChanged("Adress");
                }
            }
        }

        public String Mail
        {
            get { return mail; }
            set
            {
                if (mail != value)
                {
                    mail = value;
                    OnPropertyChanged("Mail");
                }
            }
        }

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        public DateTime DoB
        {
            get { return dob; }
            set
            {
                if (dob != value)
                {
                    dob = value;
                    OnPropertyChanged("DoB");
                }
            }
        }

        public String BloodType
        {
            get { return bloodType; }
            set
            {
                if(bloodType != value)
                {
                    bloodType = value;
                    OnPropertyChanged("BloodType");
                }
            }
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if(doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }

        public ObservableCollection<Report> Reports
        {
            get { return reports; }
            set
            {
                if (reports != value)
                {
                    reports = value;
                    OnPropertyChanged("Reports");
                }
            }
        }

        public ObservableCollection<String> Allergens
        {
            get { return allergens; }
            set
            {
                if(allergens != value)
                {
                    allergens = value;
                    OnPropertyChanged("Allergens");
                }
            }
        }

    }
}

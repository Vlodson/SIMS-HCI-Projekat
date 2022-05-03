using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HospitalMain.Enums;
using System.Collections.ObjectModel;
using HospitalMain.Model;

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

        private BloodType bloodType;
        private ObservableCollection<Report> reports;
        private ObservableCollection<Allergens> allergens;
        private ObservableCollection<Notification> notifications;


        //public MedicalRecord(ObservableCollection<Report> reports)
        //{
        //    this.reports=reports;
        //}

        public MedicalRecord(String id, String ucin, String name, String surname, String phone_number, String mail, String adress, Gender gender, DateTime dob, BloodType bloodType, ObservableCollection<Report> reports, ObservableCollection<Allergens> allergens, ObservableCollection<Notification> notifications)
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
            this.bloodType = bloodType;
            this.reports = reports;
            this.allergens = allergens;
            this.notifications = notifications;
        }

        public String ID
        {
            get { return id; }
            set
            {
                if(id != value)
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
                if(ucin != value)
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
                if(name != value)
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
                if(surname != value)
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

        public BloodType BloodType
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

        public ObservableCollection<Allergens> Allergens
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

        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                notifications = value;
            }
        }

        public MedicalRecord()
        {

        }

    }
}

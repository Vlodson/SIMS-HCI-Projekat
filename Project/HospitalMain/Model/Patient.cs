using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HospitalMain.Enums;

namespace Model
{
    public class Patient : Guest, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
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
        private DateTime doB;

        private ObservableCollection<Examination> examinations;
        private String medicalRecordID;

        public Patient(Guest guest) : base(guest.ID)
        {

        }

        public Patient() : base()
        {

        }

        public Patient(String id, String ucin, String name, String surname, String phone_number, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, ObservableCollection<Examination> examinations)
        {
            this.id = id;
            this.ucin = ucin;
            this.name = name;
            this.surname = surname;
            this.phone_number = phone_number;
            this.mail = mail;
            this.adress = adress;
            this.gender = gender;
            this.doB = doB;
            this.medicalRecordID = medicalRecordID; 
            this.examinations = examinations;
        }

        public String ID
        {
            get
            {
                return id;
            }
            set
            {
                if(value != id)
                {
                    id = value;
                    //OnPropertyChanged("ID");
                }
            }
        }

        public String UCIN
        {
            get
            {
                return ucin;
            }
            set
            {
                if (value != ucin)
                {
                    ucin = value;
                    OnPropertyChanged("UCIN");
                }
            }

        }

        public String MedicalRecordID
        {
            get
            {
                return medicalRecordID;
            }
            set
            {
                if (value != medicalRecordID)
                {
                    medicalRecordID = value;
                    OnPropertyChanged("MedicalRecordID");
                }
            }

        }

        public String Name
        {
            get { return name; }
            set
            {
                if(value != name)
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
                if((phone_number != value))
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
                if(adress != value)
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
                if(mail != value)
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
                if(gender != value)
                {
                    gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        public DateTime DoB
        {
            get { return doB; }
            set
            {
                if(doB != value)
                {
                    doB = value;
                    OnPropertyChanged("DoB");
                }
            }
        }

        public ObservableCollection<Examination> Examinations
        {
            get{ return examinations; }
            set
            {
                if(examinations != value)
                {
                    examinations = value;
                    OnPropertyChanged("Examinations");
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
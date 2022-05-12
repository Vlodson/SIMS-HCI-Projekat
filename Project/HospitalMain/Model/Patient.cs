using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HospitalMain.Enums;
using HospitalMain.Model;

namespace Model
{
    public class Patient : UserAccount, INotifyPropertyChanged
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
        private String medicalRecordID;
        private bool isGuest;
        private List<Answer> answers;
        private String currentMonth;
        private int numberCanceling;
        private int numberNewExams;

        //public Patient(Guest guest) : base(guest.ID)
        //{

        //}

        public Patient(String id, String ucin, String name, String surname, String phone_number, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, bool isGuest, List<Answer> answers, String currentMonth, int numberCanceling, int numberNewExams)
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
            this.isGuest = isGuest;
            this.answers = answers;
            this.currentMonth = currentMonth;
            this.numberCanceling = numberCanceling;
            this.numberNewExams = numberNewExams;
        }

        public Patient()
        {

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

        public bool IsGuest
        {
            get { return isGuest; }
            set
            {
                if(value != isGuest)
                {
                    isGuest = value;
                    OnPropertyChanged("IsGuest");
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

        public List<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }

        public String CurrentMonth
        {
            get
            {
                return currentMonth;
            }
            set
            {
                currentMonth = value;
                OnPropertyChanged("CurrentMonth");
            }
        }

        public int NumberCanceling
        {
            get
            {
                return numberCanceling;
            }
            set
            {
                numberCanceling = value;
                OnPropertyChanged("NumberCanceling");
            }
        }

        public int NumberNewExams
        {
            get
            {
                return numberNewExams;
            }
            set
            {
                numberNewExams = value;
                OnPropertyChanged("NumberNewExams");
            }
        }
        
        public override string ToString()
        {
            return Name;
        }

        
    }
}
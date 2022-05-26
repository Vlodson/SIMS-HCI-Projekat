using HospitalMain.Controller;
using HospitalMain.Model;
using HospitalMain.Repository;
using Patient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class PersonalNotificationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private PersonalNotificationController _personalNotificationController;

        private String text;
        private int hours;
        private int minutes;
        private List<String> days;
        private String monday;
        private String tuesday;
        private String wednesday;
        private String thursday;
        private String friday;
        private String saturday;
        private String sunday;

        public MyICommand AddPersonalNotificationCommand { get; set; }

        private Window thisWindow;

        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public int Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public String Monday
        {
            get
            {
                return monday;
            }
            set
            {
                monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public String Tuesday
        {
            get
            {
                return tuesday;
            }
            set
            {
                tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public String Wednesday
        {
            get
            {
                return wednesday;
            }
            set
            {
                wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        public String Thursday
        {
            get
            {
                return thursday;
            }
            set
            {
                thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        public String Friday
        {
            get
            {
                return friday;
            }
            set
            {
                friday = value;
                OnPropertyChanged("Friday");
            }
        }

        public String Saturday
        {
            get
            {
                return saturday;
            }
            set
            {
                saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

        public String Sunday
        {
            get
            {
                return sunday;
            }
            set
            {
                sunday = value;
                OnPropertyChanged("Sunday");
            }
        }
        public PersonalNotificationViewModel(Window window)
        {
            App app = Application.Current as App;
            _personalNotificationController = app.personalNotificationController;

            AddPersonalNotificationCommand = new MyICommand(OnAddPersonalNotification);


            Hours = 0;
            Minutes = 0;

            thisWindow = window;
        }

        public void OnAddPersonalNotification()
        {
            List<int> selectedDays = new List<int>();
            if (Monday == "pon")
            {
                selectedDays.Add(1);
            }
            if (Tuesday == "uto")
            {
                selectedDays.Add(2);
            }
            if (Wednesday == "sre")
            {
                selectedDays.Add(3);
            }
            if (Thursday == "čet")
            {
                selectedDays.Add(4);
            }
            if (Friday == "pet")
            {
                selectedDays.Add(5);
            }
            if (Saturday == "sub")
            {
                selectedDays.Add(6);
            }
            if (Sunday == "ned")
            {
                selectedDays.Add(7);
            }
            PersonalNotification personalNotification = new PersonalNotification(Login.loggedId, Text, selectedDays, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hours, Minutes, 0));
            _personalNotificationController.AddPersonalNotification(personalNotification);
            thisWindow.Close();
        }
    }
}

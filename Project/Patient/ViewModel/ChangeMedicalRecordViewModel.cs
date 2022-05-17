using Controller;
using Model;
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
    public class ChangeMedicalRecordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicalRecordController _medicalRecordController;

        private String name;
        private String surname;
        private String address;
        private String phone;

        private Window thisWindow;

        public MyICommand RequestCommand { get; set; }

        public String Name
        {
            get
            {
                    return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public String Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public String Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public ChangeMedicalRecordViewModel(Window window)
        {
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            thisWindow = window;

            RequestCommand = new MyICommand(OnRequest, CanRequest);

            MedicalRecord medicalRecord = _medicalRecordController.GetMedicalRecord(Login.loggedId);
            Name = medicalRecord.Name;
            Surname = medicalRecord.Surname;
            Address = medicalRecord.Adress;
            Phone = medicalRecord.PhoneNumber;
        }

        private bool CanRequest()
        {
            if(Name != null && Surname != null && Address != null && Phone != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnRequest()
        {
            thisWindow.Close();
        }
    }
}

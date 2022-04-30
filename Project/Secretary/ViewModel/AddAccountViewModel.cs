using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Controller;
using HospitalMain.Enums;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;

namespace Secretary.ViewModel
{
    public class AddAccountViewModel : ViewModelBase
    {
        private PatientController _patientController;

        //UCIN
        private String _ucin;
        public String UCIN
        {
            get { return _ucin; }
            set { _ucin = value; OnPropertyChanged(nameof(UCIN));  }
        }

        //Name
        private String _name;
        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        //Surname
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        //Adress
        private String _adress;
        public String Adress
        {
            get { return _adress; }
            set { _adress = value; OnPropertyChanged(nameof(Adress)); }
        }

        //DateOfBirth
        private DateTime _dateOfBirth = new DateTime(1970, 1, 1);
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); }
        }

        //Mail
        private String _mail;
        public String Mail
        {
            get { return _mail; }
            set { _mail = value; OnPropertyChanged(nameof(Mail)); }
        }

        //Gender
        private List<ComboBoxData<Gender>> genderType = new List<ComboBoxData<Gender>>();
        public List<ComboBoxData<Gender>> GenderType
        {
            get { return genderType; }
            set { genderType = value; OnPropertyChanged(nameof(GenderType)); }
        }

        private Gender _gender;
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        private void FillGenderTypeComboBoxData()
        {
            foreach(Gender gender in Enum.GetValues<Gender>())
            {
                genderType.Add(new ComboBoxData<Gender> { Name = gender.ToString(), Value = gender });
            }
        }

        //MedicalRecordID
        private String _medicalRecordID;
        public String MedicalRecordID
        {
            get { return _medicalRecordID; }
            set { _medicalRecordID = value; OnPropertyChanged(nameof(MedicalRecordID));}
        }

        //PhoneNumber
        private String _phoneNumber;
        public String PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private Window _addAccount;
        public ICommand AddCommand { get; }

        public AddAccountViewModel(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel, Window addAccount)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

            _addAccount = addAccount;

            FillGenderTypeComboBoxData();

            //jos inicijalizacija komande i u xamlu binding
            AddCommand = new AddAccountCommand(this, cRUDAccountOptionsViewModel, _patientController, addAccount);
        }
    }
}

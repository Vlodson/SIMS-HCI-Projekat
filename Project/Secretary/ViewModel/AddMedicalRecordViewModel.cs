using Controller;
using HospitalMain.Enums;
using HospitalMain.Model;
using Model;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Secretary.ViewUtils;

namespace Secretary.ViewModel
{
    public class AddMedicalRecordViewModel : ViewModelBase
    {
        private MedicalRecordController _medicalRecordController;

        //Ime
        private String _name;
        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        //Prezime
        private String _surname;
        public String Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        //JMBG
        private String _ucin;
        public String UCIN
        {
            get { return _ucin; }
            set { _ucin = value; OnPropertyChanged(nameof(UCIN)); }
        }

        //Pol
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

        //Datum rodjenja
        private DateTime _dateOfBirth = new DateTime(1970, 1, 1);
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); }
        }

        //Adresa
        private String _adress;
        public String Adress
        {
            get { return _adress; }
            set { _adress = value; OnPropertyChanged(nameof(Adress)); }
        }

        //Mail
        private String _mail;
        public String Mail
        {
            get { return _mail; }
            set { _mail = value; OnPropertyChanged(nameof(Mail)); }
        }

        //Broj telefona
        private String _phoneNumber;
        public String PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        //Krvna grupa
        private List<ComboBoxData<BloodType>> bloodTypeComboBoxData = new List<ComboBoxData<BloodType>>();
        public List<ComboBoxData<BloodType>> BloodTypeComboBoxData
        {
            get { return bloodTypeComboBoxData; }
            set { bloodTypeComboBoxData = value; OnPropertyChanged(nameof(BloodTypeComboBoxData)); }
        }

        private BloodType _bloodType;
        public BloodType BloodType
        {
            get { return _bloodType; }
            set { _bloodType = value; OnPropertyChanged(nameof(BloodType)); }
        }

        private void FillBloodTypeComboBoxData()
        {
            foreach(BloodType bloodType in Enum.GetValues<BloodType>())
            {
                bloodTypeComboBoxData.Add(new ComboBoxData<BloodType> { Name = bloodType.ToString(), Value = bloodType });
            }
        }

        //Alergeni
        private ObservableCollection<SelectableItemWrapper<Allergens>> allergensListBoxData = new ObservableCollection<SelectableItemWrapper<Allergens>>();
        public ObservableCollection<SelectableItemWrapper<Allergens>> AllergensListBoxData
        {
            get { return allergensListBoxData; }
            set { allergensListBoxData = value; OnPropertyChanged(nameof(AllergensListBoxData)); }
        }

        private ObservableCollection<Allergens> _allergens;
        public ObservableCollection<Allergens> Allergens
        {
            get { return _allergens; }
            set { _allergens = value; OnPropertyChanged(nameof(Allergens)); }
        }

        private void FillAllergensListBox()
        {
            foreach(Allergens allergen in Enum.GetValues<Allergens>())
            {
                allergensListBoxData.Add(new SelectableItemWrapper<Allergens> { IsSelected = false, Item = allergen});
            }
        }

        //Izvestaji
        private ObservableCollection<Report> _reports;
        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { _reports = value; OnPropertyChanged(nameof(Reports)); }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddMedicalRecordViewModel(CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordsViewModel medicalRecordsViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            Allergens = new ObservableCollection<Allergens>();

            FillGenderTypeComboBoxData();
            FillBloodTypeComboBoxData();
            FillAllergensListBox();

            AddCommand = new AddMedicalRecordCommand(this, cRUDMedicalRecordViewModel, _medicalRecordController, medicalRecordsViewModel);
            CancelCommand = new CancelMedRecordCommand(medicalRecordsViewModel);
        }

    }
}

using Controller;
using HospitalMain.Enums;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class CreateGuestViewModel : ViewModelBase
    {
        private PatientController _patientController;

        public ICommand CreateGuestCommand { get; }
        public ICommand CancelCommand { get; }

        //Name
        private String _name;
        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        //Surname
        private String _surname;
        public String Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
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
            foreach (Gender gender in Enum.GetValues<Gender>())
            {
                genderType.Add(new ComboBoxData<Gender> { Name = gender.ToString(), Value = gender });
            }
        }

        public CreateGuestViewModel(EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

            FillGenderTypeComboBoxData();

            //komande
            CreateGuestCommand = new CreateGuestCommand(this, _patientController, emergencyGeneralViewModel);
            CancelCommand = new CancelGuestCommand(emergencyGeneralViewModel);
        }
    }
}

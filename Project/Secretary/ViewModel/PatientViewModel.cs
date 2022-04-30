using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using HospitalMain.Enums;

namespace Secretary.ViewModel
{
    public class PatientViewModel : ViewModelBase
    {

        private readonly Patient _patient;

        public String ID => _patient.ID;
        public String UCIN => _patient.UCIN;
        public String Name => _patient.Name;
        public String Surname => _patient.Surname;
        public String PhoneNumber => _patient.PhoneNumber;
        public String Mail =>_patient.Mail;
        public String Adress => _patient.Adress;
        public Gender Gender => _patient.Gender;
        public String DateOfBirth => _patient.DoB.ToString("d");
        public String MedicalRecordID => _patient.MedicalRecordID;


        public PatientViewModel(Patient patient)
        {
            _patient = patient;
        }

    }
}

using HospitalMain.Enums;
using HospitalMain.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class MedicalRecordViewModel : ViewModelBase
    {
        private readonly MedicalRecord _medicalRecord;

        public String ID => _medicalRecord.ID;
        public String UCIN => _medicalRecord.UCIN;
        public String Name => _medicalRecord.Name;
        public String Surname => _medicalRecord.Surname;
        public String PhoneNumber => _medicalRecord.PhoneNumber;
        public String Mail => _medicalRecord.Mail;
        public String Adress => _medicalRecord.Adress;
        public Gender Gender => _medicalRecord.Gender;
        public String DateOfBirth => _medicalRecord.DoB.ToString("d");
        public BloodType BloodType => _medicalRecord.BloodType;
        public ObservableCollection<Report> Reports => _medicalRecord.Reports;
        public ObservableCollection<Allergens> Allergens => _medicalRecord.Allergens;


        public MedicalRecordViewModel(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }
    }
}

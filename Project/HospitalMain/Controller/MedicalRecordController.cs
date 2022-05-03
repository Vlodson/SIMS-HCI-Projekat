using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;
using HospitalMain.Model;

namespace Controller
{
    public class MedicalRecordController
    {

        private MedicalRecordService medRecordService;

        public MedicalRecordController(MedicalRecordService medRecordService)
        {
            this.medRecordService = medRecordService;
        }

        public int generateID()
        {
            return medRecordService.generateID();
        }

        public bool CreateMedicalRecord(String medRecordID, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime dob, BloodType bloodType, ObservableCollection<Report> reports, ObservableCollection<Allergens> allergens)
        {
            return medRecordService.CreateMedicalRecord(medRecordID, ucin, name, surname, phoneNum, mail, adress, gender, dob, bloodType, reports, allergens);
        }

        public void EditMedicalRecord(String medRecordID, String newUCIN, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, BloodType newBloodType, ObservableCollection<Report> newReports, ObservableCollection<Allergens> newAllergens)
        {
            medRecordService.EditMedicalRecord(medRecordID, newUCIN, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newBloodType, newReports, newAllergens);
        }

        public bool DeleteMedicalRecord(String medRecordID)
        {
            return medRecordService.DeleteMedicalRecord(medRecordID);
        }

        public MedicalRecord GetMedicalRecord(String medRecordID)
        {
            return medRecordService.GetMedicalRecord(medRecordID);
        }

        public ObservableCollection<MedicalRecord> GetAllMedicalRecords()
        {
            return medRecordService.GetAllMedicalRecords();
        }
    }
}

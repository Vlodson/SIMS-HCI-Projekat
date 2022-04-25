using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;
using HospitalMain.Model;

namespace Service
{
    public class MedicalRecordService
    {

        private MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService(MedicalRecordRepo medicalRecordRepo)
        {
            this.medicalRecordRepo = medicalRecordRepo;
        }

        public bool CreateMedicalRecord(String medRecordID, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime dob, String bloodType, ObservableCollection<Report> reports, ObservableCollection<Allergen> allergens)
        {
            return medicalRecordRepo.NewMedicalRecord(new MedicalRecord(medRecordID, ucin, name, surname, phoneNum, mail, adress, gender, dob, bloodType, reports, allergens));
        }

        public void EditMedicalRecord(String medRecordID, String newUCIN, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, String newBloodType, ObservableCollection<Report> reports, ObservableCollection<Allergen> newAllergens)
        {
            medicalRecordRepo.EditMedicalRecord(medRecordID, new MedicalRecord(medRecordID, newUCIN, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newBloodType, reports, newAllergens));
        }

        public bool DeleteMedicalRecord(String medRecordID)
        {
            return medicalRecordRepo.DeleteMedicalRecord(medRecordID);
        }

        public MedicalRecord GetMedicalRecord(String medRecordID)
        {
            return medicalRecordRepo.ReadMedicalRecord(medRecordID);
        }

        public ObservableCollection<MedicalRecord> GetAllMedicalRecords()
        {
            return medicalRecordRepo.ReadAllMedicalRecords();
        }


    }
}

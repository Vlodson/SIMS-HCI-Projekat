using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Repository;
using Enums;
using Utility;

namespace Service
{
    public class MedicineService
    {
        private readonly MedicineRepo _repository;

        public MedicineService(MedicineRepo medicineRepo)
        {
            _repository = medicineRepo;
        }

        public void NewMedicine(Medicine medicine)
        {
            _repository.NewMedicine(medicine);
        }

        public Medicine GetMedicine(String medicineId)
        {
            return _repository.GetMedicine(medicineId);
        }

        public void SetMedicine(Medicine medicine)
        {
            _repository.SetMedicine(medicine);
        }

        public bool DeleteMedicine(String medicineId)
        {
            return _repository.DeleteMedicine(medicineId);
        }
        public ObservableCollection<Medicine> ReadAllPending(String id)
        {
            ObservableCollection<Medicine> pendingMedicines = new ObservableCollection<Medicine>();
            foreach(Medicine medicine in _repository.Medicine)
            {
                if (medicine.Status.ToString().Equals(StatusEnum.Pending.ToString()) && id.Equals(medicine.ReviewingDoctor))
                    pendingMedicines.Add(medicine);
            }

            return pendingMedicines;
        }
        public ObservableCollection<Medicine> ReadAllApproved()
        {
            ObservableCollection<Medicine> approvedMedicines = new ObservableCollection<Medicine>();
            foreach (Medicine medicine in _repository.Medicine)
            {
                if (medicine.Status.ToString().Equals(StatusEnum.Pending.ToString()))
                    approvedMedicines.Add(medicine);
            }

            return approvedMedicines;
        }

        // call to see which possible allergies to medication exist
        public List<MedicineTypeEnum> PossibleMedicineAllergens()
        {
            return new List<MedicineTypeEnum>(ReplacementMedicineMap.ReplacemetMedicine.Keys);
        }

        // call if patient is allergic to any of the drugs in the list of possible drug allergies
        public MedicineTypeEnum ReplacementMedicine(Medicine medicine)
        {
            return ReplacementMedicineMap.ReplacemetMedicine[medicine.Type];
        }

        public ObservableCollection<Medicine> ReadAll()
        {
            return _repository.Medicine;
        }
        public bool CheckAllergens(Medicine medicine, MedicalRecord medicalRecord)
        {
            foreach(var allergen in medicalRecord.Allergens)
            {
                if(medicine.Type.ToString().Equals(allergen.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadMedicine()
        {
            _repository.LoadMedicine();
        }

        public void SaveMedicine()
        {
            _repository.SaveMedicine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Model;
using Service;

namespace Controller
{
    public class MedicineController
    {
        private readonly MedicineService _medicineService;

        public MedicineController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public void NewMedicine(Medicine medicine)
        {
            _medicineService.NewMedicine(medicine);
        }

        public Medicine GetMedicine(String medicineId)
        {
            return _medicineService.GetMedicine(medicineId);
        }

        public void SetMedicine(Medicine medicine)
        {
            _medicineService.SetMedicine(medicine);
        }

        public bool DeleteMedicine(String medicineId)
        {
            return _medicineService.DeleteMedicine(medicineId);
        }
        public ObservableCollection<Medicine> ReadAllPending(String id)
        {
            return _medicineService.ReadAllPending(id);
        }
        public ObservableCollection<Medicine> ReadAllApproved()
        {
            return _medicineService.ReadAllApproved();
        }

        public ObservableCollection<Medicine> ReadAll()
        {
            return _medicineService.ReadAll();
        }
        public bool CheckAllergens(Medicine medicine, MedicalRecord medicalRecord)
        {
            return _medicineService.CheckAllergens(medicine, medicalRecord);
        }

        public String GenerateID()
        {
            return _medicineService.GenerateID();
        }

        public static String GenerateName(MedicineTypeEnum type)
        {
            return MedicineService.GeneranteName(type);
        }

        public ObservableCollection<Medicine> QueryMedicine(String query)
        {
            return _medicineService.QueryMedicine(query);
        }

        public void LoadMedicine()
        {
            _medicineService.LoadMedicine();
        }

        public void SaveMedicine()
        {
            _medicineService.SaveMedicine();
        }
    }
}

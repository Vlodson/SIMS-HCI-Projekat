using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Repository;

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

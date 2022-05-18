using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Medicine> ReadAllPending()
        {
            ObservableCollection<Medicine> pendingMedicines = new ObservableCollection<Medicine>();
            foreach(Medicine medicine in _repository.Medicine)
            {
                if (medicine.Status.ToString().Equals("Pending"))
                    pendingMedicines.Add(medicine);
            }
            return pendingMedicines;
        }

        public ObservableCollection<Medicine> ReadAll()
        {
            return _repository.Medicine;
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

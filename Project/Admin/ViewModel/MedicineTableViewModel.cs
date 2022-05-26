using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using Enums;
using HospitalMain.Enums;

namespace Admin.ViewModel
{
    public class MedicineTableViewModel: BindableBase
    {
        private MedicineController medicineController;
        public ObservableCollection<FriendlyMedicine> Medicines { get; set; }
        public FriendlyMedicine SelectedMedicine { get; set; }

        public MedicineTableViewModel()
        {
            var app = Application.Current as App;
            medicineController = app.medicineController;

            ObservableCollection<Medicine> medicines = medicineController.ReadAll();

            Medicines = new ObservableCollection<FriendlyMedicine>();
            foreach (Medicine medicine in medicines)
                Medicines.Add(new FriendlyMedicine(medicine));
        }

        public void RemoveMedicine()
        {
            medicineController.DeleteMedicine(SelectedMedicine.Id);
            Medicines.Remove(SelectedMedicine);
        }
    }

    public class FriendlyMedicine
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public MedicineTypeEnum Type { get; set; }
        public StatusEnum Status { get; set; }

        public FriendlyMedicine(Medicine medicine)
        {
            Id = medicine.Id;
            Name = medicine.Name;
            Type = medicine.Type;
            Status = medicine.Status;
        }
    }
}

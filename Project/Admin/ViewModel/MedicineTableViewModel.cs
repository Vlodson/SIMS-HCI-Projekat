using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

namespace Admin.ViewModel
{
    public class MedicineTableViewModel: BindableBase
    {
        private MedicineController medicineController;
        public ObservableCollection<Medicine> Medicines { get; set; }

        public MedicineTableViewModel()
        {
            var app = Application.Current as App;
            medicineController = app.medicineController;

            ObservableCollection<Medicine> medicines = medicineController();
        }
    }
}

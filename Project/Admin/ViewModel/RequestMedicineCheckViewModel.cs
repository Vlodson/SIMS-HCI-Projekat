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
using Enums;


namespace Admin.ViewModel
{
    public class RequestMedicineCheckViewModel: BindableBase
    {
        private MedicineController _medicineController;
        private DoctorController _doctorController;
        private String ingredients;
        private String type;
        private Medicine selectedMedicine;

        public ObservableCollection<Medicine> Medicines { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public Doctor SelectedDoctor { get; set; }

        public String Ingredients
        {
            get { return ingredients; }
            set
            {
                if(ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }

        public String Type
        {
            get { return type; }
            set
            {
                if(type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");

                Type = selectedMedicine.Type.ToString();
                Ingredients = "";
                foreach(IngredientEnum ingredient in selectedMedicine.Ingredients)
                    Ingredients += ingredient.ToString() + "\n";
                Ingredients = Ingredients.Remove(Ingredients.Length-1);
            }
        }

        public RequestMedicineCheckViewModel()
        {
            var app = Application.Current as App;
            _medicineController = app.medicineController;
            _doctorController = app.doctorController;

            Medicines = _medicineController.ReadAll();
            Doctors = _doctorController.GetAll();
        }
    }
}

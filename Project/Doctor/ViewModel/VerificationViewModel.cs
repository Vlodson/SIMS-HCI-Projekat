using Commands;
using Controller;
using Doctor;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class VerificationViewModel : ViewModelBase
    {
        public ObservableCollection<Medicine> Medicines { get; set; }
        private MedicineController _medicineController;
        private MedicineRepo _medicineRepo;
        private Medicine selectedMedicine;
        private string ingredients;
        public MyICommand SendCommand { get; set; }
        private bool isChecked1;
        private bool isChecked2;

        public VerificationViewModel()
        {
            var app = System.Windows.Application.Current as App;
            _medicineController = app.medicineController;
            _medicineRepo = app.medicineRepo;
            SendCommand = new MyICommand(OnSend);
            Medicines = _medicineController.ReadAllPending(MainWindow._uid);

            
        }
        public bool IsChecked1
        {
            get { return isChecked1; }
            set
            {
                if (isChecked1 != value)
                {
                    isChecked1 = value;
                    OnPropertyChanged("IsChecked1");
                }
            }
        }
        public bool IsChecked2
        {
            get { return isChecked2; }
            set
            {
                if (isChecked2 != value)
                {
                    isChecked2 = value;
                    OnPropertyChanged("IsChecked2");
                }
            }
        }
        public string Ingredients
        {
            get { return ingredients; }
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }
        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                if (selectedMedicine != value)
                {
                    selectedMedicine = value;
                    OnPropertyChanged("SelectedMedicine");
                    IngredientsToString();
                }
            }
        }
        public void IngredientsToString()
        {
            Ingredients = "";
            for (int i = 0; i < selectedMedicine.Ingredients.Count; i++)
            {
                Ingredients += selectedMedicine.Ingredients[i].ToString();
                if (i != selectedMedicine.Ingredients.Count - 1)
                    Ingredients += ", ";
            }
        }
        public void OnSend()
        {
            if (IsChecked1)
                selectedMedicine.Status = Enums.StatusEnum.Approved;
            else
                selectedMedicine.Status = Enums.StatusEnum.Rejected;
            _medicineRepo.SaveMedicine();
        }
    }
}

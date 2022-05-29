using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class RequestMedicineCheckViewModel: BindableBase
    {
        public ICommandTemplate SendCommand { get; private set; }
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate FillCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private MedicineController _medicineController;
        private DoctorController _doctorController;
        private String ingredients;
        private String type;
        private Medicine selectedMedicine;
        private DateTime arrivalDate;
        private String comment;

        public List<Medicine> MedicineList { get; set; }
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
                if (selectedMedicine != value)
                {
                    selectedMedicine = value;
                    OnPropertyChanged("SelectedMedicine");

                    Type = selectedMedicine.Type.ToString();
                    Ingredients = "";
                    foreach (IngredientEnum ingredient in selectedMedicine.Ingredients)
                        Ingredients += ingredient.ToString() + "\n";
                    Ingredients = Ingredients.Remove(Ingredients.Length - 1);

                    ArrivalDate = DateTime.Parse(selectedMedicine.ArrivalDate.ToString());
                }
            }
        }

        public DateTime ArrivalDate
        {
            get { return arrivalDate; }
            set
            {
                arrivalDate = value;
                OnPropertyChanged("ArrivalDate");
            }
        }

        public String Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public RequestMedicineCheckViewModel()
        {
            SendCommand = new ICommandTemplate(OnSend, CanSend);
            FillCommand = new ICommandTemplate(OnFill);
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);

            var app = Application.Current as App;
            _medicineController = app.medicineController;
            _doctorController = app.doctorController;

            MedicineList = new List<Medicine>(_medicineController.ReadAll());

            Medicines = new ObservableCollection<Medicine>(MedicineList.Where(m => m.Status == StatusEnum.Pending));
            Doctors = _doctorController.GetAllDoctors();
            ArrivalDate = DateTime.Now;
        }

        public void OnSend()
        {
            // TODO: send to doctor
            MessageBox.Show("Request sent");
            mainWindow.Width = 750;
            mainWindow.Height = 430;
            mainWindow.CurrentView = new MainMenuView();
        }

        public bool CanSend()
        {
            return (SelectedMedicine is not null && SelectedDoctor is not null && Comment is not null);
        }

        public void OnFill()
        {
            // TODO
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    // delete request
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "home":
                    // delete request
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    // delete request
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
            }
        }

    }
}

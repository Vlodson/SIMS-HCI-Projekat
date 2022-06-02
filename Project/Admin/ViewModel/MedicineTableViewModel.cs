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

using Admin.Views;

namespace Admin.ViewModel
{
    public class MedicineTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private MedicineController medicineController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        private ObservableCollection<FriendlyMedicine> medicines;
        private FriendlyMedicine selectedMedicine;

        public String Search
        {
            get { return search; }
            set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        public ObservableCollection<FriendlyMedicine> Medicines
        {
            get { return medicines; }
            set
            {
                if(medicines != value)
                {
                    medicines = value;
                    OnPropertyChanged("Medicines");
                }
            }
        }

        public FriendlyMedicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                if(selectedMedicine != value)
                {
                    selectedMedicine = value;
                    OnPropertyChanged("SelectedMedicine");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public MedicineTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            medicineController = app.medicineController;

            ObservableCollection<Medicine> medicines = medicineController.ReadAll();

            Medicines = new ObservableCollection<FriendlyMedicine>();
            foreach (Medicine medicine in medicines)
                Medicines.Add(new FriendlyMedicine(medicine));

            Search = "Enter Query";
        }

        public void OnRemove()
        {
            medicineController.DeleteMedicine(SelectedMedicine.Id);
            Medicines.Remove(SelectedMedicine);
        }

        public bool CanRemove()
        {
            return selectedMedicine is not null;
        }

        public void OnQuery()
        {
            Medicines.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Medicine> medicines = medicineController.ReadAll();
                foreach (Medicine medicineItem in medicines)
                    Medicines.Add(new FriendlyMedicine(medicineItem));
                return;
            }

            ObservableCollection<Medicine> queriedMedicine = medicineController.QueryMedicine(Search);
            foreach (Medicine queriedMedicineItem in queriedMedicine)
                Medicines.Add(new FriendlyMedicine(queriedMedicineItem));
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "rooms":
                    mainWindow.CurrentView = new RoomTableView();
                    break;
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
                case "transfers":
                    mainWindow.CurrentView = new EquipmentTransferTableView();
                    break;
                case "renovations":
                    mainWindow.CurrentView = new RenovationTableView();
                    break;
            }
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

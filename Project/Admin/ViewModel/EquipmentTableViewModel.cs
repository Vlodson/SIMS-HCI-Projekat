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

using Admin.Views;

namespace Admin.ViewModel
{
    public class EquipmentTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private EquipmentController equipmentController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        public String Search
        {
            get { return search; }
            set
            {
                if (search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        private ObservableCollection<FriendlyEquipment> equipment;
        public ObservableCollection<FriendlyEquipment> Equipment
        {
            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }
        private FriendlyEquipment selectedEquipment;
        public FriendlyEquipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipment");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public EquipmentTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            equipmentController = app.equipmentController;

            ObservableCollection<Equipment> equipment = equipmentController.ReadAll();

            // there has to be a better way lol
            Equipment = new ObservableCollection<FriendlyEquipment>();
            foreach(Equipment equipmentItem in equipment)
                Equipment.Add(new FriendlyEquipment(equipmentItem));

            Search = "Enter Query";
        }

        public void OnRemove()
        {
            equipmentController.RemoveEquipment(SelectedEquipment.Id, SelectedEquipment.RoomId);
            Equipment.Remove(SelectedEquipment);
        }
        public bool CanRemove()
        {
            return SelectedEquipment is not null;
        }

        public void OnQuery()
        {
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Equipment> equipment = equipmentController.ReadAll();
                Equipment.Clear();
                foreach (Equipment equipmentItem in equipment)
                    Equipment.Add(new FriendlyEquipment(equipmentItem));
                return;
            }

            ObservableCollection<Equipment> queriedEquipment = equipmentController.QueryEquipment(Search);
            Equipment.Clear();
            foreach(Equipment queriedItem in queriedEquipment)
                Equipment.Add(new FriendlyEquipment(queriedItem));
            
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
                case "medicine":
                    mainWindow.CurrentView = new MedicineTableView();
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

    public class FriendlyEquipment
    {
        public String Id { get; set; }
        public String RoomId { get; set; }
        public String Type { get; set; }

        public FriendlyEquipment(Equipment equipment)
        {
            Id = equipment.Id;
            RoomId = equipment.RoomId;
            Type = EquipmentTypeEnumExtensions.ToFriendlyString(equipment.Type);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}

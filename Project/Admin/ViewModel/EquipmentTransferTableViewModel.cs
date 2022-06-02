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
    public class EquipmentTransferTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private EquipmentTransferController equipmentTransferController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private ObservableCollection<FriendlyEquipmentTransfer> equipmentTransfers;
        public ObservableCollection<FriendlyEquipmentTransfer> EquipmentTransfers
        {
            get { return equipmentTransfers; }
            set
            {
                if(equipmentTransfers != value)
                {
                    equipmentTransfers = value;
                    OnPropertyChanged("EquipmentTransfers");
                }
            }
        }

        private FriendlyEquipmentTransfer selectedEquipment;
        public FriendlyEquipmentTransfer SelectedEquipmentTransfer
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipmentTransfer");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

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

        public EquipmentTransferTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            equipmentTransferController = app.equipmentTransferController;

            ObservableCollection<EquipmentTransfer> equipmentTransfers = equipmentTransferController.ReadAll();

            EquipmentTransfers = new ObservableCollection<FriendlyEquipmentTransfer>();
            foreach (EquipmentTransfer equipmentTransfer in equipmentTransfers)
                EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransfer));

            Search = "Enter Query";
        }

        public void OnRemove()
        {
            equipmentTransferController.DeleteEquipmentTransfer(SelectedEquipmentTransfer.Id);
            EquipmentTransfers.Remove(SelectedEquipmentTransfer);
        }
        public bool CanRemove()
        {
            return SelectedEquipmentTransfer is not null;
        }
        public void OnQuery()
        {
            EquipmentTransfers.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<EquipmentTransfer> equipmentTransfers = equipmentTransferController.ReadAll();
                foreach (EquipmentTransfer equipmentTransferItem in equipmentTransfers)
                    EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransferItem));
                return;
            }

            ObservableCollection<EquipmentTransfer> queriedEquipmentTransfers = equipmentTransferController.QueryEquipmentTransfers(Search);
            foreach (EquipmentTransfer equipmentTransferItem in queriedEquipmentTransfers)
                EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransferItem));

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
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
                case "renovations":
                    mainWindow.CurrentView = new RenovationTableView();
                    break;
            }
        }

    }

    public class FriendlyEquipmentTransfer
    {
        public String Id { get; set; }
        public int OriginRoom { get; set; }
        public int DestinationRoom { get; set; }
        public String Equipment { get; set; }
        public DateTime EndDate { get; set; }

        public FriendlyEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            Id = equipmentTransfer.Id;
            OriginRoom = equipmentTransfer.OriginRoom.RoomNb;
            DestinationRoom = equipmentTransfer.DestinationRoom.RoomNb;
            Equipment = EquipmentTypeEnumExtensions.ToFriendlyString(equipmentTransfer.Equipment.Type);
            EndDate = equipmentTransfer.EndDate;
        }
    }
}

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
    public class RecordEquipmentTransferViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private EquipmentTransferController equipmentTransferController;
        private EquipmentTransfer equipmentTransfer;
        private String title;
        private String equipment;
        private String destination;
        private String startDate;
        private String endDate;

        // properties
        public String Title
        {
            get { return title; }
            set
            {
                if(title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public String Equipment
        {
            get { return equipment; }
            set
            {
                if(equipment != value)
                {
                    equipment = value;
                    OnPropertyChanged("Equipment");
                }
            }
        }
        public String Destination
        {
            get { return destination; }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    OnPropertyChanged("Destination");
                }
            }
        }
        public String StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        public String EndDate
        {
            get { return endDate; }
            set
            {
                if(endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public RecordEquipmentTransferViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            SaveCommand = new ICommandTemplate(OnSave);

            var app = Application.Current as App;
            equipmentTransferController = app.equipmentTransferController;

            equipmentTransfer = equipmentTransferController.ReadAll().Last();

            Title = "Record transfer for room\n" + equipmentTransfer.OriginRoom.RoomNb;
            Equipment = EquipmentTypeEnumExtensions.ToFriendlyString(equipmentTransfer.Equipment.Type);
            Destination = equipmentTransfer.DestinationRoom.RoomNb.ToString();
            StartDate = equipmentTransfer.StartDate.ToString();
            EndDate = equipmentTransfer.EndDate.ToString();
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
                    mainWindow.CurrentView = new ScheduleEquipmentTransferView();
                    break;
                case "home":
                    equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "save":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
            }
        }

        public void OnSave()
        {
            equipmentTransferController.RecordTransfer(equipmentTransfer.Id);
            OnNavigation("save");
        }
    }
}

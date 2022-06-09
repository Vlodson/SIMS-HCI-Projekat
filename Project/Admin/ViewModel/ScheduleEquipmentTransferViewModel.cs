using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class ScheduleEquipmentTransferViewModel: BindableBase
    {
        // commands
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate FillCommand { get; private set; }
        public ICommandTemplate SelectRoomCommand { get; private set; }
        public ICommandTemplate RecordCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }

        // controlers
        private EquipmentTransferController equipmentTransferController;
        private RoomController roomController;
        private EquipmentController equipmentController;

        // private fields
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private ObservableCollection<FriendlyEquipment> availableEquipment;
        private String title;
        private Room destinationRoom;
        private FriendlyEquipment selectedEquipment;
        private String selectedRoomNb;
        private DateTime startDate;
        private DateTime endDate;
        private String startTime;
        private String endTime;


        // properties
        public ObservableCollection<FriendlyEquipment> AvailableEquipment // to show in combobox, keys from the map above
        {
            get { return availableEquipment; }
            set
            {
                if(availableEquipment != value)
                {
                    availableEquipment = value;
                    OnPropertyChanged("AvailableEquipment");
                }
            }
        } 
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

        public Room DestinationRoom
        {
            get { return destinationRoom; }
            set
            {
                if(destinationRoom != value)
                {
                    destinationRoom = value;
                    OnPropertyChanged("DestinationRoom");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public FriendlyEquipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipment");
                }
            }
        }

        public String SelectedRoomNb
        {
            get { return selectedRoomNb; }
            set
            {
                if(selectedRoomNb != value)
                {
                    selectedRoomNb = value;
                    OnPropertyChanged("SelectedRoomNb");
                }
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if(startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
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

        public String StartTime
        {
            get { return startTime; }
            set
            {
                if(startTime != value)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public String EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    OnPropertyChanged("EndTime");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        // constructor and methods
        public ScheduleEquipmentTransferViewModel()
        {
            // command init
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            FillCommand = new ICommandTemplate(OnFill);
            SelectRoomCommand = new ICommandTemplate(OnSelect);
            RecordCommand = new ICommandTemplate(OnRecord, CanRecordSave);
            SaveCommand = new ICommandTemplate(OnSave, CanRecordSave);

            // controller init
            var app = Application.Current as App;
            equipmentTransferController = app.equipmentTransferController;
            roomController = app.roomController;
            equipmentController = app.equipmentController;

            // set initial field values
            Room originRoom = roomController.GetClipboardRoom();
            Title = "Schedule equipment transfer for room\n" + originRoom.RoomNb;
            SelectedRoomNb = "Select destination";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            AvailableEquipment = new ObservableCollection<FriendlyEquipment>();
            foreach (Equipment equipment in originRoom.Equipment)
                AvailableEquipment.Add(new FriendlyEquipment(equipment));

            StartTime = "HH:MM";
            EndTime = "HH:MM";
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.CurrentView = new ChooseFormView();
                    break;
                case "home":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "record":
                    MessageBox.Show(mainWindow, "Transfer successfully scheduled");
                    mainWindow.CurrentView = new RecordEquipmentTransferView();
                    break;
            }
        }

        public void OnFill()
        {
            EquipmentTransfer equipmentTransfer = equipmentTransferController.GetClipboardEquipmentTransfer();
            // cant set selected equipment cuz the room might not have it

            // check if saved room exists at the time of filling
            SelectedRoomNb = roomController.ReadRoom(equipmentTransfer.Id) is not null ? equipmentTransfer.DestinationRoom.RoomNb.ToString() : "Saved room doesnt exist";
            DestinationRoom = roomController.ReadRoom(equipmentTransfer.Id) is not null ? equipmentTransfer.DestinationRoom : null;

            StartDate = equipmentTransfer.StartDate;
            StartTime = StartDate.TimeOfDay.ToString();
            EndDate = equipmentTransfer.EndDate;
            EndTime = EndDate.TimeOfDay.ToString();
        }

        public void OnSelect()
        {
            mainWindow.Width = 750;
            mainWindow.Height = 430;
            mainWindow.CurrentView = new HospitalLayoutSubmenuView(mainWindow.CurrentView, this, "transfer");
        }

        public void OnRecord()
        {
            // generate ID
            Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
            Room originRoom = roomController.GetClipboardRoom();
            DestinationRoom = roomController.GetSelectedRoom();
            DateTime start = ChangeTime(StartDate, TimeOnly.Parse(StartTime));
            DateTime end = ChangeTime(EndDate, TimeOnly.Parse(EndTime));
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer(equipmentTransferController.GenerateID(), originRoom, DestinationRoom, equipment, start, end);
            equipmentTransferController.ScheduleTransfer(equipmentTransfer);
            OnNavigation("record"); 
        }

        public void OnSave()
        {
            // generate ID
            Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
            Room originRoom = roomController.GetClipboardRoom();
            DestinationRoom = roomController.GetSelectedRoom();
            DateTime start = ChangeTime(StartDate, TimeOnly.Parse(StartTime));
            DateTime end = ChangeTime(EndDate, TimeOnly.Parse(EndTime));
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer(equipmentTransferController.GenerateID(), originRoom, DestinationRoom, equipment, start, end);
            equipmentTransferController.SetClipboardEquipmentTransfer(equipmentTransfer);
            // dont turn off
        }

        public bool CanRecordSave()
        {
            bool can_record = false;
            if (DestinationRoom is not null)
            {
                Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
                Room originRoom = roomController.GetClipboardRoom();
                EquipmentTransfer equipmentTransfer = new EquipmentTransfer("0", originRoom, DestinationRoom, equipment, StartDate, EndDate);
                can_record = equipmentTransferController.OccupiedAtTheTime(equipmentTransfer);
            }

            // can_record &&
            return StartDate >= DateTime.Now.Date && EndDate >= StartDate && !String.IsNullOrEmpty(StartTime) && !String.IsNullOrEmpty(EndTime);
        }

        private DateTime ChangeTime(DateTime date, TimeOnly time)
        {
            return new DateTime(
                date.Year,
                date.Month,
                date.Day,
                time.Hour,
                time.Minute,
                time.Second
                );
        }
    }
}

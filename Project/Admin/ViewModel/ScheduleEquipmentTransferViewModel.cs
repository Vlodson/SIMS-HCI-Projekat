using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Admin.View;
using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;

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
        private Room destinationRoom;
        private FriendlyEquipment selectedEquipment;
        private String selectedRoomNb;
        private DateTime startDate;
        private DateTime endDate;

        // public fields
        public String Title;
        public ObservableCollection<FriendlyEquipment> AvailableEquipment; // to show in combobox, keys from the map above

        // properties
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
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    break;
                case "home":
                    break;
                case "logout":
                    break;
                case "discard":
                    break;
                case "record":
                    break;
            }
        }

        public void OnFill()
        {
            EquipmentTransfer equipmentTransfer = equipmentTransferController.GetClipboardEquipmentTransfer();
            // cant set selected equipment cuz the room might not have it

            // check if saved room exists at the time of filling
            SelectedRoomNb = roomController.ReadRoom(equipmentTransfer.Id) is not null ? equipmentTransfer.DestinationRoom.RoomNb.ToString() : "Saved room doesnt exist";
            destinationRoom = roomController.ReadRoom(equipmentTransfer.Id) is not null ? equipmentTransfer.DestinationRoom : null;

            StartDate = equipmentTransfer.StartDate;
            EndDate = equipmentTransfer.EndDate;
        }

        public void OnSelect()
        {
            // change view to submenu
            if(roomController.GetSelectedRoom() is not null)
            {
                destinationRoom = roomController.GetSelectedRoom();
                SelectedRoomNb = destinationRoom.RoomNb.ToString();
            }
        }

        public void OnRecord()
        {
            // generate ID
            Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
            Room originRoom = roomController.GetClipboardRoom();
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer("0", originRoom, destinationRoom, equipment, StartDate, EndDate);
            equipmentTransferController.ScheduleTransfer(equipmentTransfer);
            OnNavigation("record"); 
        }

        public void OnSave()
        {
            // generate ID
            Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
            Room originRoom = roomController.GetClipboardRoom();
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer("0", originRoom, destinationRoom, equipment, StartDate, EndDate);
            equipmentTransferController.SetClipboardEquipmentTransfer(equipmentTransfer);
            // dont turn off
        }

        public bool CanRecordSave()
        {
            bool can_record = false;
            if (destinationRoom is not null)
            {
                Equipment equipment = equipmentController.ReadEquipment(SelectedEquipment.Id);
                Room originRoom = roomController.GetClipboardRoom();
                EquipmentTransfer equipmentTransfer = new EquipmentTransfer("0", originRoom, destinationRoom, equipment, StartDate, EndDate);
                can_record = equipmentTransferController.OccupiedAtTheTime(equipmentTransfer);
            }

            return can_record && StartDate >= DateTime.Now && EndDate >= StartDate && destinationRoom != null;
        }
    }
}

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
    public class RecordEquipmentTransferViewModel
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }

        private EquipmentTransferController equipmentTransferController;
        private EquipmentTransfer equipmentTransfer;

        public String Title;
        public String Equipment;
        public String Destination;
        public String StartDate;
        public String EndDate;

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
                    break;
                case "home":
                    break;
                case "logout":
                    break;
                case "discard":
                    break;
            }
        }

        public void OnSave()
        {
            equipmentTransferController.RecordTransfer(equipmentTransfer.Id);
            OnNavigation("home");
        }
    }
}

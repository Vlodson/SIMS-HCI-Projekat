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

namespace Admin.ViewModel
{
    public class EquipmentTransferTableViewModel: BindableBase
    {
        private EquipmentTransferController equipmentTransferController;
        public ObservableCollection<FriendlyEquipmentTransfer> EquipmentTransfers { get; set; }
        public FriendlyEquipmentTransfer SelectedEquipmentTransfer { get; set; }

        public EquipmentTransferTableViewModel()
        {
            var app = Application.Current as App;
            equipmentTransferController = app.equipmentTransferController;

            ObservableCollection<EquipmentTransfer> equipmentTransfers = equipmentTransferController.ReadAll();

            EquipmentTransfers = new ObservableCollection<FriendlyEquipmentTransfer>();
            foreach (EquipmentTransfer equipmentTransfer in equipmentTransfers)
                EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransfer));
        }

        public void RemoveEquipmentTransfer()
        {
            equipmentTransferController.DeleteEquipmentTransfer(SelectedEquipmentTransfer.Id);
            EquipmentTransfers.Remove(SelectedEquipmentTransfer);
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

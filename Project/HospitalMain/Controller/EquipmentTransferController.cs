using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Service;
using Model;

namespace Controller
{
    public class EquipmentTransferController
    {
        private readonly EquipmentTransferService _equipmentTransferService;

        public EquipmentTransferController(EquipmentTransferService equipmentTransferService)
        {
            _equipmentTransferService = equipmentTransferService;
        }

        public bool ScheduleTransfer(EquipmentTransfer equipmentTransfer)
        {
            return _equipmentTransferService.ScheduleTransfer(equipmentTransfer);
        }

        public bool RecordTransfer(String trainsferId)
        {
            return _equipmentTransferService.RecordTransfer(trainsferId);
        }

        public bool OccupiedAtTheTime(EquipmentTransfer equipmentTransfer)
        {
            return _equipmentTransferService.OccupiedAtTheTime(equipmentTransfer);
        }

        public bool DeleteEquipmentTransfer(String equipmentTransferId)
        {
            return _equipmentTransferService.DeleteEquipmentTransfer(equipmentTransferId);
        }

        public bool SetClipboardEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            return _equipmentTransferService.SetClipboardEquipmentTransfer(equipmentTransfer);
        }
        public EquipmentTransfer GetClipboardEquipmentTransfer()
        {
            return _equipmentTransferService.GetClipboardEquipmentTransfer();
        }
        public ObservableCollection<EquipmentTransfer> ReadAll()
        {
            return _equipmentTransferService.ReadAll();
        }
        public bool LoadEquipmentTransfer()
        {
            return _equipmentTransferService.LoadEquipmentTransfer();
        }
        public bool SaveEquipmentTransfer()
        {
            return _equipmentTransferService.SaveEquipmentTransfer();
        }
    }
}

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

        public bool ScheduleTransfer(String id, String originRoomId, String destinationRoomId, String equipmentId, DateOnly startDate, DateOnly endDate)
        {
            return _equipmentTransferService.ScheduleTransfer(id, originRoomId, destinationRoomId, equipmentId, startDate, endDate);
        }

        public bool RecordTransfer(String trainsferId, String signature)
        {
            return _equipmentTransferService.RecordTransfer(trainsferId, signature);
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

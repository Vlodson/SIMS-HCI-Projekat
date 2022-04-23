using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository;
using Model;

namespace Service
{
    public class EquipmentTransferService
    {
        private readonly EquipmentTransferRepo _equipmentTransferRepo;
        private readonly RoomRepo _roomRepo;
        private readonly EquipmentRepo _equipmentRepo;

        public EquipmentTransferService(EquipmentTransferRepo equipmentTransferRepo, RoomRepo roomRepo, EquipmentRepo equipmentRepo)
        {
            _equipmentTransferRepo = equipmentTransferRepo;
            _roomRepo = roomRepo;
            _equipmentRepo = equipmentRepo;
        }

        public bool ScheduleTransfer(String id, String originRoomId, String destinationRoomId, String equipmentId, DateOnly startDate, DateOnly endDate)
        {
            Room originRoom = _roomRepo.GetRoom(originRoomId);
            Room destinationRoom = _roomRepo.GetRoom(destinationRoomId);
            Equipment equipment = _equipmentRepo.GetEquipment(equipmentId);

            // remove from origin
            if (!_roomRepo.RemoveEquipment(originRoom.Id, equipment.Id))
                return false;

            // add to destination
            if (!_roomRepo.AddEquipment(destinationRoom.Id, equipment))
                return false;

            // change equipment room id
            _equipmentRepo.SetEquipment(equipment.Id, destinationRoom.Id, equipment.Type);

            // make new schedule with no signature, cuz thats recording
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer(id, originRoom, destinationRoom, equipment, startDate, endDate, "");
            _equipmentTransferRepo.NewEquipmentTransfer(equipmentTransfer);

            return true;
        }

        public bool RecordTransfer(String trainsferId, String signature)
        {
            EquipmentTransfer equipmentTransfer = _equipmentTransferRepo.GetEquipmentTransfer(trainsferId);
            _equipmentTransferRepo.SetEquipmentTransfer(trainsferId, equipmentTransfer.OriginRoom, equipmentTransfer.DestinationRoom, equipmentTransfer.Equipment, equipmentTransfer.StartDate, equipmentTransfer.EndDate, signature);
            return true;
        }

        public bool DeleteEquipmentTransfer(String equipmentTransferId)
        {
            return _equipmentTransferRepo.DeleteEquipmentTransfer(equipmentTransferId);
        }

        public bool SetClipboardEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            return _equipmentTransferRepo.SetClipboardEquipmentTransfer(equipmentTransfer);
        }

        public EquipmentTransfer GetClipboardEquipmentTransfer()
        {
            return _equipmentTransferRepo.GetClipboardEquipmentTransfer();
        }

        public ObservableCollection<EquipmentTransfer> ReadAll()
        {
            return _equipmentTransferRepo.equipmentTransfers;
        }

        public bool LoadEquipmentTransfer()
        {
            return _equipmentTransferRepo.LoadEquipmentTransfer();
        }

        public bool SaveEquipmentTransfer()
        {
            return _equipmentTransferRepo.SaveEquipmentTransfer();
        }

    }
}

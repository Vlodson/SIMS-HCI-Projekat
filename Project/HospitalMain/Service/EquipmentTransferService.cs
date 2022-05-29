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
        private readonly ExaminationRepo _examinationRepo;

        public EquipmentTransferService(EquipmentTransferRepo equipmentTransferRepo, RoomRepo roomRepo, EquipmentRepo equipmentRepo, ExaminationRepo examinationRepo)
        {
            _equipmentTransferRepo = equipmentTransferRepo;
            _roomRepo = roomRepo;
            _equipmentRepo = equipmentRepo;
            _examinationRepo = examinationRepo;
        }

        public bool ScheduleTransfer(EquipmentTransfer equipmentTransfer)
        {
            // make new schedule with no signature, cuz thats recording, and thats when the actual transfer happens
            _equipmentTransferRepo.NewEquipmentTransfer(equipmentTransfer);

            return true;
        }

        public bool RecordTransfer(String trainsferId)
        {
            EquipmentTransfer equipmentTransfer = _equipmentTransferRepo.GetEquipmentTransfer(trainsferId);

            // add to destination
            if (!_roomRepo.AddEquipment(equipmentTransfer.DestinationRoom.Id, equipmentTransfer.Equipment))
                return false;

            // remove from origin
            if (!_roomRepo.RemoveEquipment(equipmentTransfer.OriginRoom.Id, equipmentTransfer.Equipment.Id))
                return false;

            // change equipment room id
            _equipmentRepo.SetEquipment(equipmentTransfer.Equipment);

            // legacy code that worked with signature, that doesnt exist anymore
            _equipmentTransferRepo.SetEquipmentTransfer(equipmentTransfer); 
            return true;
        }

        public bool OccupiedAtTheTime(EquipmentTransfer equipmentTransfer)
        {
            foreach(Examination examination in _examinationRepo.ExaminationList)
            {
                if (equipmentTransfer.OriginRoom.Id == examination.ExamRoomId || equipmentTransfer.DestinationRoom.Id == examination.ExamRoomId)
                    if (equipmentTransfer.StartDate >= examination.Date && equipmentTransfer.EndDate <= examination.Date.AddMinutes(examination.Duration))
                        return false;
                   
            }

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

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using Model;
using Utility;
using Repository;

namespace Repository
{
    public class EquipmentTransferRepo
    {
        public String dbPath { get; set; }
        private RoomRepo _roomRepo;
        private EquipmentRepo _equipmentRepo;
        public ObservableCollection<EquipmentTransfer> equipmentTransfers { get; set; }
        public EquipmentTransfer clipboardEquipmentTransfer { get; set; }

        public EquipmentTransferRepo(String db_path, RoomRepo roomRepo, EquipmentRepo equipmentRepo)
        {
            dbPath = db_path;
            _roomRepo = roomRepo;
            _equipmentRepo = equipmentRepo;
            equipmentTransfers = new ObservableCollection<EquipmentTransfer>();
        }

        public bool NewEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            equipmentTransfers.Add(equipmentTransfer);
            return true;
        }

        public EquipmentTransfer GetEquipmentTransfer(String equipmentTransferId)
        {
            foreach(EquipmentTransfer equipmentTransfer in equipmentTransfers)
                if(equipmentTransfer.Id.Equals(equipmentTransferId))
                    return equipmentTransfer;

            return null;
        }

        public void SetEquipmentTransfer(EquipmentTransfer newEquipmentTransfer)
        {
            for(int i = 0; i < equipmentTransfers.Count; i++)
            {
                if (equipmentTransfers[i].Id.Equals(newEquipmentTransfer.Id))
                {
                    equipmentTransfers[i].OriginRoom = newEquipmentTransfer.OriginRoom;
                    equipmentTransfers[i].DestinationRoom = newEquipmentTransfer.DestinationRoom;
                    equipmentTransfers[i].Equipment = newEquipmentTransfer.Equipment;
                    equipmentTransfers[i].StartDate = newEquipmentTransfer.StartDate;
                    equipmentTransfers[i].EndDate = newEquipmentTransfer.EndDate;
                    break;
                }
            }
        }

        public bool DeleteEquipmentTransfer(String equipmentTransferId)
        {
            foreach(EquipmentTransfer equipmentTransfer in equipmentTransfers)
                if (equipmentTransfer.Id.Equals(equipmentTransferId))
                {
                    equipmentTransfers.Remove(equipmentTransfer);
                    return true;
                }

            return false;
        }

        public bool SetClipboardEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            clipboardEquipmentTransfer = equipmentTransfer;
            return true;
        }

        public EquipmentTransfer GetClipboardEquipmentTransfer()
        {
            return clipboardEquipmentTransfer;
        }

        public bool LoadEquipmentTransfer()
        {
            using FileStream fileStream = File.OpenRead(dbPath);

            List<EquipmentTransferAnnotation> equipmentTransferAnnotations = JsonSerializer.Deserialize<List<EquipmentTransferAnnotation>>(fileStream);

            foreach (EquipmentTransferAnnotation equipmentTransferAnnotation in equipmentTransferAnnotations)
            {
                EquipmentTransfer equipmentTransfer = new EquipmentTransfer(equipmentTransferAnnotation);
                equipmentTransfer.OriginRoom = _roomRepo.GetRoom(equipmentTransferAnnotation.OriginRoomId);
                equipmentTransfer.DestinationRoom = _roomRepo.GetRoom(equipmentTransferAnnotation.DestinationRoomId);
                equipmentTransfer.Equipment = _equipmentRepo.GetEquipment(equipmentTransferAnnotation.EquipmentId);
            }

            return true;
        }

        public bool SaveEquipmentTransfer()
        {
            List<EquipmentTransferAnnotation> equipmentTransferAnnotations = new List<EquipmentTransferAnnotation>();
            foreach (EquipmentTransfer equipmentTransfer in equipmentTransfers)
                equipmentTransferAnnotations.Add(new EquipmentTransferAnnotation(equipmentTransfer));

            String jsonString = JsonSerializer.Serialize(equipmentTransferAnnotations);
            File.WriteAllText(dbPath, jsonString);

            return true;
        }

    }
}

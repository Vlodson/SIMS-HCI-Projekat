using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Repository;
using HospitalMain.Enums;

namespace Service
{
    public class EquipmentService
    {

        private readonly EquipmentRepo _equipmentRepo;

        public EquipmentService(EquipmentRepo equipmentRepo)
        {
            _equipmentRepo = equipmentRepo;
        }

        public bool CreateEquipment(String equipmentId, String roomId, EquipmentTypeEnum type)
        {
            Equipment equipment = new Equipment(equipmentId, roomId, type);
            return _equipmentRepo.NewEquipment(equipment);
        }

        public bool RemoveEquipment(String equipmentId)
        {
            return _equipmentRepo.DeleteEquipment(equipmentId);
        }

        public void EditEquipment(String equipmentId, String newRoomId, EquipmentTypeEnum newType)
        {
            Equipment equipment = new Equipment(equipmentId, newRoomId, newType);
            _equipmentRepo.SetEquipment(equipmentId, equipment);
        }

        public Equipment ReadEquipment(String equipmentId)
        {
            return _equipmentRepo.GetEquipment(equipmentId);
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentRepo.Equipment;
        }

        public bool LoadEquipment()
        {
            return _equipmentRepo.LoadEquipment();
        }

        public bool SaveEquipment()
        {
            return _equipmentRepo.SaveEquipment();
        }
    }
}

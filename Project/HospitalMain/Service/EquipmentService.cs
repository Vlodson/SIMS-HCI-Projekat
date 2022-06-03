using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Utility;
using Repository;
using HospitalMain.Enums;

namespace Service
{
    public class EquipmentService
    {

        private readonly EquipmentRepo _equipmentRepo;
        private readonly RoomRepo _roomRepo;

        public EquipmentService(EquipmentRepo equipmentRepo, RoomRepo roomRepo)
        {
            _equipmentRepo = equipmentRepo;
            _roomRepo = roomRepo;
        }

        public bool CreateEquipment(Equipment equipment)
        {
            return _equipmentRepo.NewEquipment(equipment);
        }

        public bool RemoveEquipment(String equipmentId, String roomId)
        {
            _roomRepo.RemoveEquipment(roomId, equipmentId);
            return _equipmentRepo.DeleteEquipment(equipmentId);
        }

        public void EditEquipment(Equipment newEquipment)
        {
            _equipmentRepo.SetEquipment(newEquipment);
        }

        public Equipment ReadEquipment(String equipmentId)
        {
            return _equipmentRepo.GetEquipment(equipmentId);
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentRepo.Equipment;
        }

        public ObservableCollection<Equipment> QueryEquipment(String query)
        {
            List<Equipment> equipmentList = new List<Equipment>(_equipmentRepo.Equipment);

            ObservableCollection<Equipment> queriedEquipment = new ObservableCollection<Equipment>(QueryUtility.DoQuery<Equipment>(equipmentList, query));

            return queriedEquipment;
        }

        public String GenerateID()
        {
            return _equipmentRepo.GenerateID();
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

using HospitalMain.Model;
using HospitalMain.Repository;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalMain.Enums;

namespace HospitalMain.Service
{
    public class DynamicEquipmentService
    {

        private readonly DynamicEquipmentRepo _dynamicEquipmentRepo;
        private readonly EquipmentRepo _equipmentRepo;
        private readonly RoomRepo _roomRepo;
    
        public DynamicEquipmentService(DynamicEquipmentRepo dynamicEquipmentRepo, EquipmentRepo equipmentRepo, RoomRepo roomRepo)
        {
            _dynamicEquipmentRepo = dynamicEquipmentRepo;
            _equipmentRepo = equipmentRepo;
            _roomRepo = roomRepo;
        }

        public int generateID()
        {
            return _dynamicEquipmentRepo.generateID();
        }

        public bool NewOrder(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
           return _dynamicEquipmentRepo.OrderNewDynamicEquipmentRequest(dynamicEquipmentRequest);
        }

        public void EditOrder(string orderID, DynamicEquipmentRequest newOrder)
        {
            _dynamicEquipmentRepo.EditOrder(orderID, newOrder);
        }

        public void DeleteOrder(string orderID)
        {
            _dynamicEquipmentRepo.DeleteOrder(orderID);
        }

        public void CheckIfOrderArrived()
        {
            foreach(DynamicEquipmentRequest request in _dynamicEquipmentRepo.getAllRequests())
            {
                if(request.OrderDate.AddDays(3) < DateTime.Now)
                {
                    AddToWareHouse(request);
                    _dynamicEquipmentRepo.DeleteOrder(request.ID);
                }
            }
        }

        private void AddToWareHouse(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            //treba odraditi ovo
            int maxID = 0;
            if (_equipmentRepo.Equipment.Count > 0)
            {
                maxID = _equipmentRepo.Equipment.Max(eq => int.Parse(eq.Id)) + 1;
            }

            Room storageRoom = null;

            foreach(Room room in _roomRepo.Rooms)
            {
                if (room.Type.Equals(RoomTypeEnum.Storage_Room))
                {
                    storageRoom = room;
                    break;
                }
            }

            for(int i = 0; i < dynamicEquipmentRequest.Quantity; i++)
            {
                Equipment equipment = new Equipment(maxID.ToString(), storageRoom.Id, EquipmentTypeEnum.Expendable_Material);
                maxID++;

                _roomRepo.AddEquipment(storageRoom.Id, equipment);
            }

        }
    }
}

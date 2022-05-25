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
using System.Collections.ObjectModel;

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
            int maxID = 0;
            ObservableCollection<DynamicEquipmentRequest> equipment = getAllRequests();

            foreach (DynamicEquipmentRequest order in equipment)
            {
                int orderID = Int32.Parse(order.ID);
                if (orderID > maxID)
                {
                    maxID = orderID;
                }
            }

            return maxID + 1;
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

        public ObservableCollection<DynamicEquipmentRequest> getAllRequests()
        {
            return _dynamicEquipmentRepo.DynamicEquipment;
        }

        public void CheckIfOrderArrived()
        {
            foreach(DynamicEquipmentRequest request in getAllRequests())
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
            Room storageRoom = FindStorageRoom();
            AddEquipmentToStorageRoom(storageRoom, dynamicEquipmentRequest);
        }

        private Room FindStorageRoom()
        {
            Room storageRoom = null;

            foreach (Room room in _roomRepo.Rooms)
            {
                if (room.Type.Equals(RoomTypeEnum.Storage_Room))
                {
                    storageRoom = room;
                    break;
                }
            }

            return storageRoom;
        }

        private void AddEquipmentToStorageRoom(Room storageRoom, DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            int maxID = 0;
            if (_equipmentRepo.Equipment.Count > 0)
            {
                maxID = _equipmentRepo.Equipment.Max(eq => int.Parse(eq.Id)) + 1;
            }

            for (int i = 0; i < dynamicEquipmentRequest.Quantity; i++)
            {
                Equipment equipment = new Equipment(maxID.ToString(), storageRoom.Id, EquipmentTypeEnum.Expendable_Material);
                maxID++;

                _roomRepo.AddEquipment(storageRoom.Id, equipment);
            }
        }
    }
}

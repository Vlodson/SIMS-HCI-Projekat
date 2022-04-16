using System;
using System.Collections.ObjectModel;

using Service;
using Model;
using HospitalMain.Enums;

namespace Controller
{
   public class RoomController
   {
      
      private readonly RoomService _roomService;
      
      public RoomController(RoomService _roomService)
        {
            this._roomService = _roomService;
        }

      public bool CreateRoom(String id, int floor, int roomNb, bool occupancy, RoomTypeEnum type)
      {
            return _roomService.CreateRoom(id, floor, roomNb, occupancy, type);
      }
      
      public bool RemoveRoom(String roomId)
      {
            return _roomService.RemoveRoom(roomId);
      }
      
      public void EditRoom(String id, ObservableCollection<Equipment> newEquipment, int newFloor, int newRoomNb, bool newOccupancy, RoomTypeEnum newType)
      {
         _roomService.EditRoom(id, newEquipment, newFloor, newRoomNb, newOccupancy, newType);
      }
      
      public Room ReadRoom(String roomId)
      {
         return _roomService.ReadRoom(roomId);
      }
      
      public ObservableCollection<Room> ReadAll()
      {
         return _roomService.ReadAll();
      }

      public bool LoadRoom()
        {
            return _roomService.LoadRoom();
        }

      public bool SaveRoom()
        {
            return _roomService.SaveRoom();
        }
   }
}
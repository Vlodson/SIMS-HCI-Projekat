using Model;
using System;
using System.Collections.Generic;
using Service;
using Model;

namespace Controller
{
   public class RoomController
   {
      
      private readonly RoomService _roomService;
      
      public RoomController(RoomService _roomService)
        {
            this._roomService = _roomService;
        }

      public bool CreateRoom(String id, int floor, int roomNb, bool occupancy, String type)
      {
            return _roomService.CreateRoom(id, floor, roomNb, occupancy, type);
      }
      
      public bool RemoveRoom(String roomId)
      {
            return _roomService.RemoveRoom(roomId);
      }
      
      public void EditRoom(String id, List<Equipment> newEquipment, int newFloor, int newRoomNb, bool newOccupancy, String newType)
      {
         _roomService.EditRoom(id, newEquipment, newFloor, newRoomNb, newOccupancy, newType);
      }
      
      public Room ReadRoom(String roomId)
      {
         return _roomService.ReadRoom(roomId);
      }
      
      public List<Room> ReadAll()
      {
         return _roomService.ReadAll();
      }

   }
}
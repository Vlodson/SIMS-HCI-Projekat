using System;
using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
   public class RoomService
   {
      
      private readonly RoomRepo _repo;

      public RoomService(RoomRepo roomRepo)
        {
            _repo = roomRepo;
        }

      public bool CreateRoom(String id, int floor, int roomNb, bool occupancy, String type)
      {
            // logic for failed addition needed
            Room r = new Room(id, floor, roomNb, occupancy, type);
            return _repo.NewRoom(r);
      }
      
      public bool RemoveRoom(String roomId)
      {
         return _repo.DeleteRoom(roomId);
      }
      
      public void EditRoom(String id, List<Equipment> newEquipment, int newFloor, int newRoomNb, bool newOccupancy, String newType)
      {
         Room room = new Room(id, newFloor, newRoomNb, newOccupancy, newType);
         room.Equipment = newEquipment;
         _repo.SetRoom(id, room);
      }
      
      public Room ReadRoom(String roomId)
      {
         return _repo.GetRoom(roomId);
      }
      
      public List<Room> ReadAll()
      {
         return _repo.Rooms;
      }
   
   }
}
using System;

namespace Service
{
   public class RoomService
   {
      public bool CreateRoom(String id, List<Equipment> equpiment, int floor, int roomNb, bool occupancy, String type)
      {
         throw new NotImplementedException();
      }
      
      public bool RemoveRoom(String roomId)
      {
         throw new NotImplementedException();
      }
      
      public void EditRoom(String id, List<Equipment> newEquipment, int newFloor, int newRoomNb, bool newOccupancy, String newType)
      {
         throw new NotImplementedException();
      }
      
      public Model.Room ReadRoom(String roomId)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> ReadAll()
      {
         throw new NotImplementedException();
      }
   
   }
}
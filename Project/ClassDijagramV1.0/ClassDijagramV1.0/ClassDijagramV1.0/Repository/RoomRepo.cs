using Model;
using Service;
using System;

namespace Repository
{
   public class RoomRepo
   {
      private String dbPath;
      
      public bool NewRoom(Room room)
      {
         throw new NotImplementedException();
      }
      
      public Room GetRoom(String roomId)
      {
         throw new NotImplementedException();
      }
      
      public void SetRoom(String roomId, Room newRoom)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteRoom(String roomId)
      {
         throw new NotImplementedException();
      }
      
      public bool LoadRoom()
      {
         throw new NotImplementedException();
      }
      
      public bool SaveRoom()
      {
         throw new NotImplementedException();
      }
      
      public RoomService roomService;
   
   }
}
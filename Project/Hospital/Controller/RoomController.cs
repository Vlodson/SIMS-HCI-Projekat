using System;

namespace Controller
{
   public class RoomController
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
      
      public System.Collections.ArrayList roomService;
      
      
      public System.Collections.ArrayList RoomService
      {
         get
         {
            if (roomService == null)
               roomService = new System.Collections.ArrayList();
            return roomService;
         }
         set
         {
            RemoveAllRoomService();
            if (value != null)
            {
               foreach (Service.RoomService oRoomService in value)
                  AddRoomService(oRoomService);
            }
         }
      }
      
      
      public void AddRoomService(Service.RoomService newRoomService)
      {
         if (newRoomService == null)
            return;
         if (this.roomService == null)
            this.roomService = new System.Collections.ArrayList();
         if (!this.roomService.Contains(newRoomService))
            this.roomService.Add(newRoomService);
      }
      
      
      public void RemoveRoomService(Service.RoomService oldRoomService)
      {
         if (oldRoomService == null)
            return;
         if (this.roomService != null)
            if (this.roomService.Contains(oldRoomService))
               this.roomService.Remove(oldRoomService);
      }
      
      
      public void RemoveAllRoomService()
      {
         if (roomService != null)
            roomService.Clear();
      }
   
   }
}
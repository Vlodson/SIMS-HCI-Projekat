using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

using Model;

namespace Repository
{
   public class RoomRepo
   {
      public String dbPath { get; set; }
      public ObservableCollection<Room> Rooms { get; set; }
      
      public RoomRepo(string db_path)
        {
            this.dbPath = db_path;
            this.Rooms = new ObservableCollection<Room>();
        }
      
      public bool NewRoom(Room room)
      {  
         // logic for when you cant add room
         Rooms.Add(room);
         return true;
      }
      
      public Room GetRoom(String roomId) // this might create problems if r is not a reference
      {
         foreach(Room r in Rooms)
                if(r.Id.Equals(roomId))
                    return r;

         return null;
      }
      
      public void SetRoom(String roomId, Room newRoom)
      {
            for(int i = 0; i < Rooms.Count; i++)
            {
                if (Rooms[i].Id.Equals(roomId))
                {
                    Rooms[i] = newRoom;
                    break;
                }
            }
      }
      
      public bool DeleteRoom(String roomId)
      {
         foreach (Room r in Rooms)
                if (r.Id.Equals(roomId))
                {
                    Rooms.Remove(r);
                    return true;
                }
         return false;
      }
      
      public bool LoadRoom()
      {
            return true;
      }
      
      public bool SaveRoom()
      {
            return true;
      }
      
   
   }
}
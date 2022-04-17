using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

using Model;
using Utility;

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

        public bool AddEquipment(String roomId, Equipment equipment)
        {
            foreach (Room room in Rooms)
                if (room.Id.Equals(roomId))
                {
                    room.Equipment.Add(equipment);
                    return true;
                }

            return false;
        }

        public bool RemoveEquipment(String roomId, string equipmentId)
        {
            foreach (Room room in Rooms)
                if (room.Id.Equals(roomId))
                {
                    foreach (Equipment equipment in room.Equipment)
                        if (equipment.Id.Equals(equipmentId))
                        {
                            room.Equipment.Remove(equipment);
                            return true;
                        }
                }

            return false;
        }
      
        public bool LoadRoom()
        {
            using FileStream roomFileStream = File.OpenRead(dbPath);
            using FileStream equipmentFileStream = File.OpenRead(GlobalPaths.EquipmentDBPath);

            ObservableCollection<Equipment> equipmentCollection = JsonSerializer.Deserialize<ObservableCollection<Equipment>>(equipmentFileStream);
            List<RoomAnnotation> roomAnnotations = JsonSerializer.Deserialize<List<RoomAnnotation>>(roomFileStream);

            foreach (RoomAnnotation roomAnnotation in roomAnnotations)
                Rooms.Add(new Room(roomAnnotation));

            foreach (Equipment equipment in equipmentCollection)
                foreach (Room room in Rooms)
                    if (room.Id.Equals(equipment.RoomId))
                        room.Equipment.Add(equipment);

            return true;
        }
      
        public bool SaveRoom()
        {
            List<RoomAnnotation> roomAnnotations = new List<RoomAnnotation>();
            foreach (Room room in Rooms)
                roomAnnotations.Add(new RoomAnnotation(room));

            string jsonString = JsonSerializer.Serialize(roomAnnotations);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }
      
   
   }
}
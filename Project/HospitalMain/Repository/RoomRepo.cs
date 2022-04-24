using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

using HospitalMain.Enums;
using Model;
using Utility;

namespace Repository
{
   public class RoomRepo
   {
        public String dbPath { get; set; }
        private EquipmentRepo _equipmentRepo;
        public ObservableCollection<Room> Rooms { get; set; }
        public Room clipboardRoom { get; set; }
        public Room selectedRoom { get; set; }
      
        public RoomRepo(string db_path, EquipmentRepo equipmentRepo)
        {
            this.dbPath = db_path;
            this._equipmentRepo = equipmentRepo;
            this.Rooms = new ObservableCollection<Room>();
        }
      
        public bool NewRoom(Room room)
        {  
            // logic for when you cant add room
            Rooms.Add(room);
            return true;
        }
      
        public Room GetRoom(String roomId)
        {
            foreach(Room r in Rooms)
                if(r.Id.Equals(roomId))
                    return r;

            return null;
        }
      
        public void SetRoom(String roomId, ObservableCollection<Equipment> newEquipment, int newFloor, int newRoomNb, bool newOccupancy, RoomTypeEnum newType)
        {
            for(int i = 0; i < Rooms.Count; i++)
            {
                if (Rooms[i].Id.Equals(roomId))
                {
                    Rooms[i].Equipment = newEquipment;
                    Rooms[i].Floor = newFloor;
                    Rooms[i].RoomNb = newRoomNb;
                    Rooms[i].Occupancy = newOccupancy;
                    Rooms[i].Type = newType;
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

        public bool SetClipboardRoom(Room room)
        {
            clipboardRoom = new Room(room);
            return true;
        }

        public Room GetClipboardRoom()
        {
            return clipboardRoom;
        }

        public bool SetSelectedRoom(Room room)
        {
            selectedRoom = room;
            return true;
        }
        public Room GetSelectedRoom()
        {
            return selectedRoom;
        }
        public void RemoveSelectedRoom()
        {
            selectedRoom = null;
        }

        public bool LoadRoom()
        {
            using FileStream roomFileStream = File.OpenRead(dbPath);

            List<RoomAnnotation> roomAnnotations = JsonSerializer.Deserialize<List<RoomAnnotation>>(roomFileStream);

            foreach (RoomAnnotation roomAnnotation in roomAnnotations)
                Rooms.Add(new Room(roomAnnotation));

            
            foreach (Equipment equipment in _equipmentRepo.Equipment)
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
using System;
using System.Collections.Generic;

namespace Model
{
   public class Room
   {
      public String Id { get; set; }
      public List<Equipment> Equipment { get; set; }
      public int Floor { get; set; }
      public int RoomNb { get; set; }
      public bool Occupancy { get; set; }
      public String Type { get; set; }

      public Room(String id, int floor, int room_nb, bool occ, string type)
        {
            Id = id;
            Equipment = new List<Equipment>();
            Floor = floor;
            RoomNb = room_nb;
            Occupancy = occ;
            Type = type;
        }

      public Room(Room r)
        {
            Id = r.Id;
            Equipment = r.Equipment;
            Floor = r.Floor;
            RoomNb = r.RoomNb;
            Occupancy = r.Occupancy;
            Type = r.Type;
        }

        public override string ToString()
        {
            return Id; //obisacu kad nadjem pametniji nacin
        }
    }
}
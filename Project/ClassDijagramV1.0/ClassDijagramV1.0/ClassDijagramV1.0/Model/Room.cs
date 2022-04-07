using System;
using System.Collections.Generic;

namespace Model
{
   public class Room
   {
      private String id { get; set; }
      private List<Equipment> equipment { get; set; }
      private int floor { get; set; }
      private int roomNb { get; set; }
      private bool occupancy { get; set; }
      private String type { get; set; }

        public Room(string id, List<Equipment> equipment, int floor, int roomNb, bool occupancy, string type)
        {
            this.id = id;
            this.equipment = equipment;
            this.floor = floor;
            this.roomNb = roomNb;
            this.occupancy = occupancy;
            this.type = type;
        }
    }
}
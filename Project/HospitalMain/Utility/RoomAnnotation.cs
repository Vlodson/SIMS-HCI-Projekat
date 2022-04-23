using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using HospitalMain.Enums;

namespace Utility
{
    public class RoomAnnotation
    {
        public String Id { get; set; }
        public List<String> Equipment { get; set; }
        public int Floor { get; set; }
        public int RoomNb { get; set; }
        public bool Occupancy { get; set; }
        public RoomTypeEnum Type { get; set; }

        public RoomAnnotation() { }
        public RoomAnnotation(Room r)
        {
            Id = r.Id;
            Floor = r.Floor;
            RoomNb = r.RoomNb;
            Occupancy = r.Occupancy;
            Type = r.Type;

            Equipment = new List<string>();
            foreach(Equipment equipment in r.Equipment)
                Equipment.Add(equipment.Id);
        }
        public RoomAnnotation(String id, List<String> equipment, int floor, int roomNb, bool occupancy, RoomTypeEnum type)
        {
            this.Id = id;
            this.Equipment = equipment;
            this.Floor = floor;
            this.RoomNb = roomNb;
            this.Occupancy = occupancy;
            this.Type = type;

        }
        public RoomAnnotation(RoomAnnotation roomAnnotation)
        {
            this.Id = roomAnnotation.Id;
            this.Equipment = roomAnnotation.Equipment;
            this.Floor = roomAnnotation.Floor;
            this.RoomNb = roomAnnotation.RoomNb;
            this.Occupancy = roomAnnotation.Occupancy;
            this.Type = roomAnnotation.Type;
        }

    }
}

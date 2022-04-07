using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
   public class Room : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private String id { get; set; }
      private List<Equipment> equipment { get; set; }
      private int floor { get; set; }
      private int roomNb { get; set; }
      private bool occupancy { get; set; }
      private String type { get; set; }

        public String Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

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
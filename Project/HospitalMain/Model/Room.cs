using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using HospitalMain.Enums;

namespace Model
{
   public class Room : INotifyPropertyChanged
   {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private String _id;
        private ObservableCollection<Equipment> _equipment;
        private int _floor;
        private int _roomnb;
        private bool _occupancy;
        private RoomTypeEnum _type;


      public String Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id"); 
                }
            }
        }
      public ObservableCollection<Equipment> Equipment { get; set; }
      public int Floor
        {
            get { return _floor; }
            set
            {
                if(_floor != value)
                {
                    _floor = value;
                    OnPropertyChanged("Floor");
                }
            }
        }
      public int RoomNb
        {
            get { return _roomnb; }
            set
            {
                if(_roomnb != value)
                {
                    _roomnb = value;
                    OnPropertyChanged("RoomNb");
                }
            }
        }
      public bool Occupancy
        {
            get { return _occupancy; }
            set
            {
                if(_occupancy != value)
                {
                    _occupancy = value;
                    OnPropertyChanged("Occupancy");
                }
            }
        }
      public RoomTypeEnum Type
        {
            get { return _type; }
            set
            {
                if(_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

      public Room() { }

      public Room(String id, int floor, int room_nb, bool occ, RoomTypeEnum type)
        {
            Id = id;
            Equipment = new ObservableCollection<Equipment>();
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
   }
}
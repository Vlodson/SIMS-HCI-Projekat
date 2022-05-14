using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using HospitalMain.Enums;
using Utility;

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
        private RoomTypeEnum _previousType;

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
        public RoomTypeEnum PreviousType
        {
            get { return _previousType; }
            set
            {
                if (_previousType != value)
                {
                    _previousType = value;
                    OnPropertyChanged("PreviousType");
                }
            }
        }

        public Room() { }

        public Room(String id, int floor, int room_nb, bool occ, RoomTypeEnum type, RoomTypeEnum previousType)
        {
            Id = id;
            Equipment = new ObservableCollection<Equipment>();
            Floor = floor;
            RoomNb = room_nb;
            Occupancy = occ;
            Type = type;
            PreviousType = previousType;
        }

        public Room(Room r)
        {
            Id = r.Id;
            Equipment = r.Equipment;
            Floor = r.Floor;
            RoomNb = r.RoomNb;
            Occupancy = r.Occupancy;
            Type = r.Type;
            PreviousType = r.PreviousType;
        }

        public Room(RoomAnnotation roomAnnotation)
        {
            this.Id = roomAnnotation.Id;
            this.Equipment = new ObservableCollection<Equipment>();
            this.Floor = roomAnnotation.Floor;
            this.RoomNb = roomAnnotation.RoomNb;
            this.Occupancy = roomAnnotation.Occupancy;
            this.Type = roomAnnotation.Type;
            this.PreviousType = roomAnnotation.PreviousType;
        }
   }
}
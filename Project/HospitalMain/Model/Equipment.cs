using System;
using System.ComponentModel;

using HospitalMain.Enums;

namespace Model
{
   public class Equipment : INotifyPropertyChanged
   {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private String _id;
        private String _roomId;
        private EquipmentTypeEnum _type;

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

        public String RoomId
        {
            get { return _roomId; }
            set
            {
                if(_roomId != value)
                {
                    _roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        public EquipmentTypeEnum Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Equipment() { }
        public Equipment(String id, String roomId, EquipmentTypeEnum type)
        {
            Id = id;
            RoomId = roomId;
            Type = type;
        }
        public Equipment(Equipment e)
        {
            Id = e.Id;
            RoomId = e.RoomId;
            Type = e.Type;
        }
   }
}
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
        public Equipment(String id, EquipmentTypeEnum type)
        {
            Id = id;
            Type = type;
        }
        public Equipment(Equipment e)
        {
            Id = e.Id;
            Type = e.Type;
        }
   }
}
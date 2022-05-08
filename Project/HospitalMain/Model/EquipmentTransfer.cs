using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Utility;

namespace Model
{
    public class EquipmentTransfer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private String _id;
        private Room _originRoom;
        private Room _destinationRoom;
        private Equipment _equipment;
        private DateOnly _startDate;
        private DateOnly _endDate;

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

        public Room OriginRoom
        {
            get { return _originRoom; }
            set
            {
                if(_originRoom != value)
                {
                    _originRoom = value;
                    OnPropertyChanged("OriginRoom");
                }
            }
        }

        public Room DestinationRoom
        {
            get { return _destinationRoom; }
            set
            {
                if(_destinationRoom != value)
                {
                    _destinationRoom = value;
                    OnPropertyChanged("DestinationRoom");
                }
            }
        }

        public Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                if(_equipment != value)
                {
                    _equipment = value;
                    OnPropertyChanged("Equipment");
                }
            }
        }

        public DateOnly StartDate
        {
            get { return _startDate; }
            set
            {
                if(_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateOnly EndDate
        {
            get { return _endDate; }
            set
            {
                if(_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public EquipmentTransfer() { }

        public EquipmentTransfer(String id, Room originRoom, Room destinationRoom, Equipment equipment, DateOnly startDate, DateOnly endDate)
        {
            Id = id;
            OriginRoom = originRoom;
            DestinationRoom = destinationRoom;
            Equipment = equipment;
            StartDate = startDate;
            EndDate = endDate;
        }

        public EquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            Id = equipmentTransfer.Id;
            OriginRoom = equipmentTransfer.OriginRoom;
            DestinationRoom = equipmentTransfer.DestinationRoom;
            Equipment = equipmentTransfer.Equipment;
            StartDate = equipmentTransfer.StartDate;
            EndDate = equipmentTransfer.EndDate;
        }

        public EquipmentTransfer(EquipmentTransferAnnotation equipmentTransferAnnotation)
        {
            this.Id = equipmentTransferAnnotation.Id;
            this.OriginRoom = null;
            this.DestinationRoom = null;
            this.Equipment = null;
            this.StartDate = DateOnly.Parse(equipmentTransferAnnotation.StartDate);
            this.EndDate = DateOnly.Parse(equipmentTransferAnnotation.EndDate);
        }
    }
}

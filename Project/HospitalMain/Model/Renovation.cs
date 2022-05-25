using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Utility;
using HospitalMain.Enums;

// parcelling logic missing
namespace Model
{
    public class Renovation : INotifyPropertyChanged
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
        private RenovationTypeEnum _type;
        private DateOnly _startDate;
        private DateOnly _endDate;

        public String Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
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
                if (_destinationRoom != value)
                {
                    _destinationRoom = value;
                    OnPropertyChanged("DestinationRoom");
                }
            }
        }

        public RenovationTypeEnum Type
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

        public Renovation() { }
        public Renovation(String id, Room originRoom, Room destinationRoom, RenovationTypeEnum type, DateOnly start, DateOnly end)
        {
            Id = id;
            OriginRoom = originRoom;
            DestinationRoom = destinationRoom;
            Type = type;
            StartDate = start;
            EndDate = end;
        }
        public Renovation(Renovation renovation)
        {
            this.Id = renovation.Id;
            this.OriginRoom = renovation.OriginRoom;
            this.DestinationRoom = renovation.DestinationRoom;
            Type = renovation.Type;
            StartDate = renovation.StartDate;
            EndDate = renovation.EndDate;
        }
        public Renovation(RenovationAnnotation renovationAnnotation)
        {
            this.Id = renovationAnnotation.Id;
            this.OriginRoom = null;
            this.DestinationRoom = null;
            this.Type = renovationAnnotation.Type;
            this.StartDate = DateOnly.Parse(renovationAnnotation.StartDate);
            this.EndDate = DateOnly.Parse(renovationAnnotation.EndDate);
        }

    }
}

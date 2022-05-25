using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using HospitalMain.Enums;

namespace Utility
{
    public class RenovationAnnotation
    {
        public String Id { get; set; }
        public String OriginRoomId { get; set; }
        public String DestinationRoomId { get; set; }
        public RenovationTypeEnum Type { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }

        public RenovationAnnotation() { }
        public RenovationAnnotation(Renovation renovation)
        {
            Id = renovation.Id;
            OriginRoomId = renovation.OriginRoom.Id;
            DestinationRoomId = renovation.DestinationRoom.Id;
            Type = renovation.Type;
            StartDate = renovation.StartDate.ToString();
            EndDate = renovation.EndDate.ToString();
        }
        public RenovationAnnotation(String id, String originRoomId, String destinationRoomId, RenovationTypeEnum type, String start, String end)
        {
            this.Id = id;
            this.OriginRoomId = originRoomId;
            this.DestinationRoomId = destinationRoomId;
            this.Type = type;
            this.StartDate = start;
            this.EndDate = end;
        }
        public RenovationAnnotation(RenovationAnnotation renovationAnnotation)
        {
            this.Id = renovationAnnotation.Id;
            this.OriginRoomId = renovationAnnotation.OriginRoomId;
            this.DestinationRoomId = renovationAnnotation.DestinationRoomId;
            this.Type = renovationAnnotation.Type;
            this.StartDate = renovationAnnotation.StartDate;
            this.EndDate = renovationAnnotation.EndDate;
        }
    }
}

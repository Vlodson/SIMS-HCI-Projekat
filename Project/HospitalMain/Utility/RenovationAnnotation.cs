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
        public RenovationTypeEnum Type { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public String Signature { get; set; }

        public RenovationAnnotation() { }
        public RenovationAnnotation(Renovation renovation)
        {
            Id = renovation.Id;
            OriginRoomId = renovation.OriginRoom.Id;
            Type = renovation.Type;
            StartDate = renovation.StartDate;
            EndDate = renovation.EndDate;
            Signature = renovation.Signature;
        }
        public RenovationAnnotation(String id, String originRoomId, RenovationTypeEnum type, DateOnly start, DateOnly end, String signature)
        {
            this.Id = id;
            this.OriginRoomId = originRoomId;
            this.Type = type;
            this.StartDate = start;
            this.EndDate = end;
            this.Signature = signature;
        }
        public RenovationAnnotation(RenovationAnnotation renovationAnnotation)
        {
            this.Id = renovationAnnotation.Id;
            this.OriginRoomId = renovationAnnotation.OriginRoomId;
            this.Type = renovationAnnotation.Type;
            this.StartDate = renovationAnnotation.StartDate;
            this.EndDate = renovationAnnotation.EndDate;
            this.Signature = renovationAnnotation.Signature;
        }
    }
}

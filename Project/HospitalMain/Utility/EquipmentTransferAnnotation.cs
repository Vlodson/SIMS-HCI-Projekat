using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Utility
{
    public class EquipmentTransferAnnotation
    {
        public String Id { get; set; }
        public String OriginRoomId { get; set; }
        public String DestinationRoomId { get; set; }
        public String EquipmentId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public String Signature { get; set; }

        public EquipmentTransferAnnotation() { }
        public EquipmentTransferAnnotation(EquipmentTransfer equipmentTransfer)
        {
            Id = equipmentTransfer.Id;
            OriginRoomId = equipmentTransfer.OriginRoom.Id;
            DestinationRoomId = equipmentTransfer.DestinationRoom.Id;
            EquipmentId = equipmentTransfer.Equipment.Id;
            StartDate = equipmentTransfer.StartDate;
            EndDate = equipmentTransfer.EndDate;
            Signature = equipmentTransfer.Signature;
        }
        public EquipmentTransferAnnotation(String id, String originId, String destinationId, String equipmentId, DateOnly start, DateOnly end, String signature)
        {
            this.Id = id;
            this.OriginRoomId = originId;
            this.DestinationRoomId = destinationId;
            this.EquipmentId = equipmentId;
            this.StartDate = start;
            this.EndDate = end;
            this.Signature = signature;
        }
        public EquipmentTransferAnnotation(EquipmentTransferAnnotation equipmentTransferAnnotation)
        {
            this.Id = equipmentTransferAnnotation.Id;
            this.OriginRoomId = equipmentTransferAnnotation.OriginRoomId;
            this.DestinationRoomId = equipmentTransferAnnotation.DestinationRoomId;
            this.StartDate= equipmentTransferAnnotation.StartDate;
            this.EndDate= equipmentTransferAnnotation.EndDate;
            this.Signature = equipmentTransferAnnotation.Signature;
        }
    }
}

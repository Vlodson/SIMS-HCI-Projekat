using HospitalMain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EquipmentRequest
    {
        private EquipmentTypeEnum equipment;
        private int amount;
        private Room room;
        private string doctorId;
        private DateTime date;

        public EquipmentRequest(EquipmentTypeEnum equipment, int amount, Room room, string doctorId, DateTime date)
        {
            this.equipment = equipment;
            this.amount = amount;
            this.room = room;
            this.doctorId = doctorId;
            this.date = date;
        }

        public EquipmentTypeEnum Equipment { get => equipment; set => equipment = value; }
        public int Amount { get => amount; set => amount = value; }
        public Room Room { get => room; set => room = value; }
        public string DoctorId { get => doctorId; set => doctorId = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}

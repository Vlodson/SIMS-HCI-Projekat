using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Enums
{
    public enum RoomTypeEnum
    {
        Patient_Room,
        Operation_Room,
        Storage_Room,
        Meeting_Room,
        Inoperative
    }

    public static class RoomTypeEnumExtensions
    {
        public static String ToFriendlyString(this RoomTypeEnum type)
        {
            switch (type)
            {
                case RoomTypeEnum.Patient_Room:
                    return "Patient Room";
                case RoomTypeEnum.Operation_Room:
                    return "Operation Room";
                case RoomTypeEnum.Storage_Room:
                    return "Storage Room";
                case RoomTypeEnum.Meeting_Room:
                    return "Meeting Room";
                case RoomTypeEnum.Inoperative:
                    return "Inoperative";
                default:
                    return "You shouldnt be here";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Enums
{
    public enum EquipmentTypeEnum
    {
        Chair,
        Table,
        Computer,
        Printer,
        Wheelchair,
        Medicine_Cabinet,
        Dish,
        Bed,
        Expendable_Material, // bandages, bandaids, oral sticks etc
        Medical_Instruments, // sthetoscope, operating instruments etc
        Scanner
    }

    public static class EquipmentTypeEnumExtensions
    {
        public static String ToFriendlyString(this EquipmentTypeEnum type)
        {
            switch (type)
            {
                case EquipmentTypeEnum.Chair:
                    return "Chair";
                case EquipmentTypeEnum.Table:
                    return "Table";
                case EquipmentTypeEnum.Computer:
                    return "Computer";
                case EquipmentTypeEnum.Printer:
                    return "Printer";
                case EquipmentTypeEnum.Wheelchair:
                    return "Wheelchair";
                case EquipmentTypeEnum.Medicine_Cabinet:
                    return "Medicine Cabinet";
                case EquipmentTypeEnum.Dish:
                    return "Dish";
                case EquipmentTypeEnum.Bed:
                    return "Bed";
                case EquipmentTypeEnum.Expendable_Material:
                    return "Expendable Material";
                case EquipmentTypeEnum.Medical_Instruments:
                    return "Medical Instruments";
                case EquipmentTypeEnum.Scanner:
                    return "Scanner";
                default:
                    return "You shouldnt be here";
            }
        }
    }
}

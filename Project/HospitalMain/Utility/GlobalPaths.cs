using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public class GlobalPaths
    {
        public static String DBPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HospitalMain\Database");
        public static String RoomsDBPath = Path.Combine(DBPath, "Rooms.json");
        public static String EquipmentDBPath = Path.Combine(DBPath, "Equipment.json");
        public static String EquipmentTransfersDBPath = Path.Combine(DBPath, "EquipmentTransfers.json");
        public static String RenovationDBPath = Path.Combine(DBPath, "Renovations.json");
        public static String ExamsDBPath = Path.Combine(DBPath, "Exams.json");
        public static String PatientsDBPath = Path.Combine(DBPath, "Patients.json");
        public static String DoctorsDBPath = Path.Combine(DBPath, "Doctors.json");
        public static String MedicalRecordDBPath = Path.Combine(DBPath, "MedicalRecords.json");
        public static String TherapyDBPath = Path.Combine(DBPath, "Therapy.json");
        public static String ReportDBPath = Path.Combine(DBPath, "Reports.json");
    }
}

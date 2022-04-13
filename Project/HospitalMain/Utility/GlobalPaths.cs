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
        public static String ExamsDBPath = Path.Combine(DBPath, "Exams.json");
        public static String PatientsDBPath = Path.Combine(DBPath, "Patients.json");
        public static String DoctorsDBPath = Path.Combine(DBPath, "Doctors.json");
    }
}

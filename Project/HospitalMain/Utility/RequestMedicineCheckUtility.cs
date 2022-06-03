using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Model;

namespace Utility
{
    public static class RequestMedicineCheckClipboard
    {
        public static String DBPath { get; set; }
        public static RequestMedicineCheckUtility ClipboardRequestMedicineCheck { get; set; }

        public static void LoadOrderProducts()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            ClipboardRequestMedicineCheck = JsonSerializer.Deserialize<RequestMedicineCheckUtility>(fileStream);
        }

        public static void SaveOrderProducts()
        {
            string jsonString = JsonSerializer.Serialize(ClipboardRequestMedicineCheck);
            File.WriteAllText(DBPath, jsonString);
        }
    }

    public class RequestMedicineCheckUtility
    {
        public String Ingredients;
        public String Type;
        public Medicine Medicine;
        public DateTime ArrivalDate;
        public String Comment;
        public Doctor Doctor;

        public RequestMedicineCheckUtility(string ingredients, string type, Medicine medicine, DateTime arrivalDate, string comment, Doctor doctor)
        {
            Ingredients = ingredients;
            Type = type;
            Medicine = medicine;
            ArrivalDate = arrivalDate;
            Comment = comment;
            Doctor = doctor;
        }

        public RequestMedicineCheckUtility(RequestMedicineCheckUtility requestMedicineCheckUtility)
        {
            Ingredients = requestMedicineCheckUtility.Ingredients;
            Type = requestMedicineCheckUtility.Type;
            Medicine = requestMedicineCheckUtility.Medicine;
            ArrivalDate = requestMedicineCheckUtility.ArrivalDate;
            Comment = requestMedicineCheckUtility.Comment;
            Doctor = requestMedicineCheckUtility.Doctor;
        }

        public RequestMedicineCheckUtility()
        {

        }

    }
}

using Enums;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface IMedicineRepo
    {
        public void NewMedicine(Medicine medicine);
        public Medicine GetMedicine(String medicineId);
        public void SetMedicine(Medicine medicine);
        public bool DeleteMedicine(String medicineId);
        public String GenerateID();
        public void LoadMedicine();
        public void SaveMedicine();

    }
}

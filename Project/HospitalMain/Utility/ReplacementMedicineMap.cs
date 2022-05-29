using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enums;

namespace Utility
{
    public static class ReplacementMedicineMap
    {
        public static Dictionary<MedicineTypeEnum, MedicineTypeEnum> ReplacemetMedicine = new Dictionary<MedicineTypeEnum, MedicineTypeEnum>
        {
            { MedicineTypeEnum.Analgesic, MedicineTypeEnum.NSAID },
            { MedicineTypeEnum.Antibiotic, MedicineTypeEnum.Antihistamine },
            { MedicineTypeEnum.Diuretic, MedicineTypeEnum.Erythromycin },
            { MedicineTypeEnum.Bronchodilator, MedicineTypeEnum.Corticosteriod },
            // cant find any for antihypertensives
            // erythromycin is the backup drug, has no allergens
            // antihistamine is the backup drug
            // NSAID is the backup drug
            // corticosteriods have no allergens
        };
    }
}

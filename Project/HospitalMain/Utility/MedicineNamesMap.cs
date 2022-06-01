using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HospitalMain.Enums;
using Enums;

namespace Utility
{
    public static class MedicineNamesMap
    {
        public static Dictionary<MedicineTypeEnum, List<String>> MedicineTypeNameMap = new Dictionary<MedicineTypeEnum, List<String>>
        {
            {MedicineTypeEnum.Analgesic, new List<String>() {"Codeine", "Fentanyl", "Hydrocodone"}},
            {MedicineTypeEnum.Antibiotic, new List<String>() {"Amoxicilin", "Cefalexin", "Tetracycline"}},
            {MedicineTypeEnum.Diuretic, new List<String>() {"Aldactone", "Bumex", "Demadex"}},
            {MedicineTypeEnum.Bronchodilator, new List<String> {"Salbutamol", "Formoterol", "Vilanterol"}},
            {MedicineTypeEnum.Antihypertensive, new List<String> {"Carvedilol", "Benzaperil", "Captropil"}},
            {MedicineTypeEnum.Erythromycin, new List<String> {"Erythrocin", "Eyemycin", "Romycin"}},
            {MedicineTypeEnum.Antihistamine, new List<String> {"Astelin", "Dimetapp", "Triaminic"}},
            {MedicineTypeEnum.NSAID, new List<String> {"Ibuprofen", "Diclofenac", "Naproxen"}},
            {MedicineTypeEnum.Corticosteriod, new List<String> {"Cortisone", "Prednisone", "Prednisolone"}}
        };
    }
}

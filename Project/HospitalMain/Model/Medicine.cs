using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Medicine
    {
        private string id;
        private string name;
        private MedicineTypeEnum type;
        private List<IngredientEnum> ingredients;
        private MedicineStatusEnum status;
        private string comment;

    }
}

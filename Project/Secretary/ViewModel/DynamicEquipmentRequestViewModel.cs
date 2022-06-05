using HospitalMain.Enums;
using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class DynamicEquipmentViewModelRequest : ViewModelBase
    {

        private readonly DynamicEquipmentRequest _request;

        public String ID => _request.ID;
        public int Quantity => _request.Quantity;
        public DynamicEquipmentTypeEnum EquipmentType => _request.EquipmentType;
        public String ShortDescription => _request.ShortDescription;
    
        public DynamicEquipmentViewModelRequest(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _request = dynamicEquipmentRequest;
        }
    }
}

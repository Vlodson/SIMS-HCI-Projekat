using HospitalMain.Enums;
using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class DynamicRequestDTOViewModel : ViewModelBase
    {

        private readonly DynamicEquipmentRequest _dynamicEquipmentRequest;

        public int Quantity => _dynamicEquipmentRequest.Quantity;
        public String EquipmentType => _dynamicEquipmentRequest.EquipmentType.ToString();

        public DynamicRequestDTOViewModel(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _dynamicEquipmentRequest = dynamicEquipmentRequest;
        }
    }
}

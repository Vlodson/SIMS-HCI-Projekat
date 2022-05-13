using HospitalMain.Model;
using HospitalMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class DynamicEquipmentController
    {
        private readonly DynamicEquipmentService _dynamicEquipmentService;

        public DynamicEquipmentController(DynamicEquipmentService dynamicEquipmentService)
        {
            _dynamicEquipmentService = dynamicEquipmentService;
        }
        
        public void CheckIfOrderArrived()
        {
            _dynamicEquipmentService.CheckIfOrderArrived();
        }

        public int generateID()
        {
            return _dynamicEquipmentService.generateID();
        }

        public bool NewOrder(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            return _dynamicEquipmentService.NewOrder(dynamicEquipmentRequest);
        }

        public void EditOrder(string orderID, DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _dynamicEquipmentService.EditOrder(orderID, dynamicEquipmentRequest);
        }

        public void DeleteOrder(string orderID)
        {
            _dynamicEquipmentService.DeleteOrder(orderID);
        }
    }
}

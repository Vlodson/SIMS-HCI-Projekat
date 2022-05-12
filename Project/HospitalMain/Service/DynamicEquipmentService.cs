using HospitalMain.Model;
using HospitalMain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class DynamicEquipmentService
    {

        private readonly DynamicEquipmentRepo _dynamicEquipmentRepo;
    
        public DynamicEquipmentService(DynamicEquipmentRepo dynamicEquipmentRepo)
        {
            _dynamicEquipmentRepo = dynamicEquipmentRepo;
        }

        public bool NewOrder(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
           return _dynamicEquipmentRepo.OrderNewDynamicEquipmentRequest(dynamicEquipmentRequest);
        }

        public void EditOrder(string orderID, DynamicEquipmentRequest newOrder)
        {
            _dynamicEquipmentRepo.EditOrder(orderID, newOrder);
        }

        public void DeleteOrder(string orderID)
        {
            _dynamicEquipmentRepo.DeleteOrder(orderID);
        }
    }
}

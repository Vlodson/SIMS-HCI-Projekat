using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Service;
using Model;
using HospitalMain.Enums;

namespace Controller
{
    public class EquipmentController
    {

        private readonly EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        public bool CreateEquipment(String equipmentId, EquipmentTypeEnum type)
        {
            return _equipmentService.CreateEquipment(equipmentId, type);
        }
        public bool RemoveEquipment(String equipmentId)
        {
            return _equipmentService.RemoveEquipment(equipmentId);
        }
        public void EditEquipment(String equipmentId, EquipmentTypeEnum newType)
        {
            _equipmentService.EditEquipment(equipmentId, newType);
        }
        public Equipment ReadEquipment(String equipmentId)
        {
            return _equipmentService.ReadEquipment(equipmentId);
        }
        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentService.ReadAll();
        }
        public bool LoadEquipment()
        {
            return _equipmentService.LoadEquipment();
        }
        public bool SaveEquipment()
        {
            return _equipmentService.SaveEquipment();
        }
    }
}

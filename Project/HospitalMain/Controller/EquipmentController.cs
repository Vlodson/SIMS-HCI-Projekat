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

        public bool CreateEquipment(String equipmentId, String roomId, EquipmentTypeEnum type)
        {
            return _equipmentService.CreateEquipment(equipmentId, roomId, type);
        }
        public bool RemoveEquipment(String equipmentId, String roomId)
        {
            return _equipmentService.RemoveEquipment(equipmentId, roomId);
        }
        public void EditEquipment(String equipmentId, String newRoomId, EquipmentTypeEnum newType)
        {
            _equipmentService.EditEquipment(equipmentId, newRoomId, newType);
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

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

        public bool CreateEquipment(Equipment equipment)
        {
            return _equipmentService.CreateEquipment(equipment);
        }
        public bool RemoveEquipment(String equipmentId, String roomId)
        {
            return _equipmentService.RemoveEquipment(equipmentId, roomId);
        }
        public void EditEquipment(Equipment newEquipment)
        {
            _equipmentService.EditEquipment(newEquipment);
        }
        public Equipment ReadEquipment(String equipmentId)
        {
            return _equipmentService.ReadEquipment(equipmentId);
        }
        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentService.ReadAll();
        }

        public ObservableCollection<Equipment> QueryEquipment(String query)
        {
            return _equipmentService.QueryEquipment(query);
        }

        public String GenerateID()
        {
            return _equipmentService.GenerateID();
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

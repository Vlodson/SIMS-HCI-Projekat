using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

namespace Admin.ViewModel
{
    public class EquipmentTableViewModel: BindableBase
    {
        private EquipmentController equipmentController;
        public ObservableCollection<FriendlyEquipment> Equipment { get; set; }
        private FriendlyEquipment selectedEquipment;
        public FriendlyEquipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipment");
                }
            }
        }
        public EquipmentTableViewModel()
        {

            var app = Application.Current as App;
            equipmentController = app.equipmentController;

            ObservableCollection<Equipment> equipment = equipmentController.ReadAll();

            // there has to be a better way lol
            Equipment = new ObservableCollection<FriendlyEquipment>();
            foreach(Equipment equipmentItem in equipment)
                Equipment.Add(new FriendlyEquipment(equipmentItem));
        }

        public void RemoveEquipment()
        {
            equipmentController.RemoveEquipment(SelectedEquipment.Id, SelectedEquipment.RoomId);
            Equipment.Remove(SelectedEquipment);
        }
    }

    public class FriendlyEquipment
    {
        public String Id { get; set; }
        public String RoomId { get; set; }
        public String Type { get; set; }

        public FriendlyEquipment(Equipment equipment)
        {
            Id = equipment.Id;
            RoomId = equipment.RoomId;
            Type = EquipmentTypeEnumExtensions.ToFriendlyString(equipment.Type);
        }
    }
}

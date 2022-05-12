using HospitalMain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class DynamicEquipmentRequest : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _id;
        private int _quantity;
        private DynamicEquipmentTypeEnum _equipmentType;
        private String _shortDescription;

        public DynamicEquipmentRequest() { }

        public DynamicEquipmentRequest(int quantity, DynamicEquipmentTypeEnum dynamicEquipmentTypeEnum, String shortDescription)
        {
            _quantity = quantity;
            _equipmentType = dynamicEquipmentTypeEnum;
            _shortDescription = shortDescription;
        }

        public String ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public DynamicEquipmentTypeEnum EquipmentType
        {
            get { return _equipmentType; }
            set { _equipmentType = value; OnPropertyChanged(nameof(EquipmentType)); }
        }

        public String ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; OnPropertyChanged(nameof(ShortDescription)); }
        }
    }
}

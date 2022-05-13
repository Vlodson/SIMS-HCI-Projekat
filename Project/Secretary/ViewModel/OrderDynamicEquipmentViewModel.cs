using HospitalMain.Controller;
using HospitalMain.Enums;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class OrderDynamicEquipmentViewModel : ViewModelBase
    {
        private readonly DynamicEquipmentController _dynamicEquipmentController;

        //dugme za narucivanje
        public ICommand OrderCommand { get; }
        //dugme za prikaz grafikona
        public ICommand ShowGraphicCommand { get; }

        //kolicina
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        //vrsta opreme
        private List<ComboBoxData<DynamicEquipmentTypeEnum>> comboBoxTypes = new List<ComboBoxData<DynamicEquipmentTypeEnum>>();
        public List<ComboBoxData<DynamicEquipmentTypeEnum>> ComboBoxTypes
        {
            get { return comboBoxTypes; }
            set { comboBoxTypes = value; OnPropertyChanged(nameof(ComboBoxTypes)); }
        }

        private DynamicEquipmentTypeEnum _equipmentType;
        public DynamicEquipmentTypeEnum EquipmentType
        {
            get { return _equipmentType; }
            set { _equipmentType = value; OnPropertyChanged(nameof(EquipmentType)); }
        }

        private void FillEquipmentTypeComboBoxData()
        {
            foreach(DynamicEquipmentTypeEnum type in Enum.GetValues<DynamicEquipmentTypeEnum>())
            {
                comboBoxTypes.Add(new ComboBoxData<DynamicEquipmentTypeEnum> { Name = type.ToString() , Value = type });
            }
        }

        //datum porucivanja
        //private DateTime _orderDateTime;
        //public DateTime OrderDateTime
        //{
        //    get { return _orderDateTime; }
        //    set { _orderDateTime = value; OnPropertyChanged(nameof(OrderDateTime)); }
        //}

        //kratak opis
        private String _shortDescription;
        public String ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; OnPropertyChanged(nameof(ShortDescription)); }
        }

        public OrderDynamicEquipmentViewModel(RequestsViewModel requestsViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _dynamicEquipmentController = app.DynamicEquipmentController;

            FillEquipmentTypeComboBoxData();

            OrderCommand = new OrderCommand(this, _dynamicEquipmentController, requestsViewModel);
            //ShowGraphicCommand = new...
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;

namespace Admin.ViewModel
{
    public class OrderProductsViewModel: BindableBase
    {
        public ICommandTemplate OrderCommand { get; private set; }
        public ICommandTemplate DiscardCommand { get; private set; }

        private MedicineController medicineController;
        private RoomController roomController;
        private EquipmentController equipmentController;

        public ObservableCollection<String> OrderType { get; set; }
        public ObservableCollection<String> ProductType { get; set; }

        private String selectedOrderType;
        private String selectedProductType;
        private String amount;
        private DateTime arrivalDate;

        public String SelectedOrderType
        {
            get { return selectedOrderType; }
            set
            {
                if(selectedOrderType != value)
                {
                    selectedOrderType = value;
                    OnPropertyChanged("SelectedOrderType");
                    OrderCommand.RaiseCanExecuteChanged();
                    
                    ProductType.Clear();

                    if (value == "Medicine")
                    {
                        var values = Enum.GetValues(typeof(MedicineTypeEnum));
                        foreach (MedicineTypeEnum val in values)
                            ProductType.Add(val.ToString());
                    }
                    else if (value == "Equipment")
                    {
                        var values = Enum.GetValues(typeof(EquipmentTypeEnum));
                        foreach (EquipmentTypeEnum val in values)
                            ProductType.Add(EquipmentTypeEnumExtensions.ToFriendlyString(val));
                    }
                }
            }
        }

        public String SelectedProductType
        {
            get { return selectedProductType; }
            set
            {
                if(SelectedProductType != value)
                {
                    selectedProductType = value;
                    OnPropertyChanged("SelectedProductType");
                    OrderCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public String Amount
        {
            get { return amount; }
            set
            {
                if(amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                    OrderCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime ArrivalDate { 
            get { return arrivalDate; }
            set
            {
                if(arrivalDate != value)
                {
                    arrivalDate = value;
                    OnPropertyChanged("ArrivalDate");
                    OrderCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public OrderProductsViewModel()
        {
            OrderCommand = new ICommandTemplate(OnOrder, CanOrder);
            DiscardCommand = new ICommandTemplate(OnDiscard);

            var app = Application.Current as App;
            medicineController = app.medicineController;
            roomController = app.roomController;
            equipmentController = app.equipmentController;

            ArrivalDate = DateTime.Now;

            OrderType = new ObservableCollection<String>();
            OrderType.Add("Medicine");
            OrderType.Add("Equipment");

            ProductType = new ObservableCollection<String>();
        }

        public void OnOrder()
        {
            if(SelectedOrderType == "Medicine")
            {
                AddMedicine();   
            }
            else if(SelectedOrderType == "Equipment")
            {
                AddEquipment();   
            }
        }

        private void AddMedicine()
        {
            List<Medicine> medicineList = new List<Medicine>(medicineController.ReadAll());
            int id = medicineList.Max(m => int.Parse(m.Id)) + 1;
            String name = "Lek" + id.ToString();

            for (int i = 0; i < int.Parse(Amount); i++)
            {
                ObservableCollection<IngredientEnum> ingredients = new ObservableCollection<IngredientEnum> { IngredientEnum.Metopropol, IngredientEnum.Cetirizine, IngredientEnum.Cipofloxacin };

                medicineController.NewMedicine(
                    new Medicine(id.ToString(),
                    name,
                    (MedicineTypeEnum)Enum.Parse(typeof(MedicineTypeEnum), SelectedProductType),
                    ingredients,
                    MedicineStatusEnum.Pending,
                    null,
                    ArrivalDate,
                    ""
                    ));

                id++;
            }
        }

        private void AddEquipment()
        {
            List<Room> roomList = new List<Room>(roomController.ReadAll());
            List<Equipment> equipmentList = new List<Equipment>(equipmentController.ReadAll());

            Room storageRoom = roomList.Where(r => r.Type == RoomTypeEnum.Storage_Room).First();
            int id = equipmentList.Max(e => int.Parse(e.Id.ToString()));

            for (int i = 0; i < int.Parse(Amount); i++)
            {
                Equipment equipment = new Equipment(id.ToString(), storageRoom.Id, (EquipmentTypeEnum)Enum.Parse(typeof(EquipmentTypeEnum), SelectedProductType));
                equipmentController.CreateEquipment(id.ToString(), storageRoom.Id, (EquipmentTypeEnum)Enum.Parse(typeof(EquipmentTypeEnum), SelectedProductType));
                roomController.AddEquipment(storageRoom.Id, equipment);
                
                id++;
            }
        }

        public bool CanOrder()
        {
            return (!String.IsNullOrEmpty(SelectedOrderType) && !String.IsNullOrEmpty(SelectedProductType) && !String.IsNullOrEmpty(Amount) && ArrivalDate >= DateTime.Today);
        }

        public void OnDiscard()
        {
            // revert view to previous
        }

    }
}

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

using Admin.Views;

namespace Admin.ViewModel
{
    public class OrderProductsViewModel: BindableBase
    {
        public ICommandTemplate OrderCommand { get; private set; }
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate FillCommand { get; private set; }

        private MedicineController medicineController;
        private RoomController roomController;
        private EquipmentController equipmentController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

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
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            FillCommand = new ICommandTemplate(OnFill);

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
                MessageBox.Show("Medicine successfully ordered");
                mainWindow.Width = 750;
                mainWindow.Height = 430;
                mainWindow.CurrentView = new MainMenuView();

            }
            else if(SelectedOrderType == "Equipment")
            {
                AddEquipment();
                MessageBox.Show("Equipment successfully ordered");
                mainWindow.Width = 750;
                mainWindow.Height = 430;
                mainWindow.CurrentView = new MainMenuView();
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
                    StatusEnum.Pending,
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

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "home":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
            }
        }

        public void OnFill()
        {

        }

    }
}

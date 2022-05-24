using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Admin.ViewModel;
using Utility;

namespace Admin.ViewModel
{
    public class TableWindowViewModel: BindableBase
    {
        public ICommandTemplate<string> ChangeTableCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private EquipmentTableViewModel equipmentTableViewModel = new EquipmentTableViewModel();
        private RoomTableViewModel roomTableViewModel = new RoomTableViewModel();
        private EquipmentTransferTableViewModel equipmentTransferTableVeiwModel = new EquipmentTransferTableViewModel();
        private RenovationTableViewModel renovationTableViewModel = new RenovationTableViewModel();
        private MedicineTableViewModel medicineTableViewModel = new MedicineTableViewModel();

        private BindableBase currentViewModel;
        private String search;

        public String Search
        {
            get { return search; }
            set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        public TableWindowViewModel()
        {
            ChangeTableCommand = new ICommandTemplate<string>(OnChange);
            RemoveCommand = new ICommandTemplate(OnRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            CurrentViewModel = equipmentTableViewModel;
            Search = "Enter Query";
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public void OnQuery()
        {
            switch (CurrentViewModel.GetType().Name)
            {
                case "EquipmentTableViewModel":
                    equipmentTableViewModel.QueryEquipment(Search);
                    currentViewModel = equipmentTableViewModel;
                    break;
            }
        }

        public void OnRemove()
        {
            // switch type of current vm and do the respeceted function there
            switch (CurrentViewModel.GetType().Name)
            {
                case "EquipmentTableViewModel":
                    equipmentTableViewModel.RemoveEquipment();
                    break;
                case "EquipmentTransferTableViewModel":
                    equipmentTransferTableVeiwModel.RemoveEquipmentTransfer();
                    break;
                case "RenovationTableViewModel":
                    renovationTableViewModel.RemoveRenovation();
                    break;
                case "MedicineTableViewModel":
                    medicineTableViewModel.RemoveMedicine();
                    break;
            }
        }

        public bool CanRemove()
        {
            // either switch type and see if something is selected or find a smarter way to get if something is selected
            return CurrentViewModel.GetType() != typeof(RoomTableViewModel); // and something is selected 
        }

        public void OnChange(string table)
        {
            switch (table)
            {
                case "equipment":
                    CurrentViewModel = equipmentTableViewModel;
                    break;
                case "rooms":
                    CurrentViewModel = roomTableViewModel;
                    break;
                case "transfers":
                    CurrentViewModel = equipmentTransferTableVeiwModel;
                    break;
                case "renovations":
                    CurrentViewModel = renovationTableViewModel;
                    break;
                case "medicines":
                    CurrentViewModel = medicineTableViewModel;
                    break;
            }
        }
    }
}

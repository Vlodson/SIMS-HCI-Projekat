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
        private EquipmentTableViewModel equipmentTableViewModel = new EquipmentTableViewModel();
        private RoomTableViewModel roomTableViewModel = new RoomTableViewModel();
        private EquipmentTransferTableViewModel equipmentTransferTableVeiwModel = new EquipmentTransferTableViewModel();
        private RenovationTableViewModel renovationTableViewModel = new RenovationTableViewModel();

        private BindableBase currentViewModel;

        public TableWindowViewModel()
        {
            ChangeTableCommand = new ICommandTemplate<string>(OnChange);
            CurrentViewModel = equipmentTableViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
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
            }
        }
    }
}

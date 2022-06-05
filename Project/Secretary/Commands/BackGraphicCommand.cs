using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class BackGraphicCommand : CommandBase
    {
        private EquipmentViewModel _equipmentViewModel;
        private MainViewModel _mainViewModel;
        public BackGraphicCommand(EquipmentViewModel equipmentViewModel, MainViewModel mainViewModel)
        {
            _equipmentViewModel = equipmentViewModel;
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Back")
            {
                _equipmentViewModel.CurrentEquipmentView = new OrderDynamicEquipmentViewModel(_equipmentViewModel, _mainViewModel);
            }
        }
    }
}

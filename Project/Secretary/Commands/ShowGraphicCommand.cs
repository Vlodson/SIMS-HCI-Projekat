using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class ShowGraphicCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        //private OrderDynamicEquipmentViewModel _dynamicViewModel;
        private EquipmentViewModel _equipmentViewModel;

        public ShowGraphicCommand(EquipmentViewModel equipmentViewModel, MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            //_dynamicViewModel = orderDynamicEquipmentView;
            _equipmentViewModel = equipmentViewModel;
        }
        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "ShowGraph")
            {
                _equipmentViewModel.CurrentEquipmentView = new GraphicViewModel(_equipmentViewModel, _mainViewModel);
            }
        }
    }
}

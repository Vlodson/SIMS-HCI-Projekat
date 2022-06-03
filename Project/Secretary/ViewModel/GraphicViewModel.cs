using HospitalMain.Controller;
using HospitalMain.Enums;
using HospitalMain.Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class GraphicViewModel : ViewModelBase
    {

        private List<DynamicRequestDTOViewModel> _equipmentRequestList;
        private ObservableCollection<DynamicRequestDTOViewModel> _equipmentList;
        private DynamicEquipmentController _dynamicEquipmentController;

        public List<DynamicRequestDTOViewModel> EquipmentRequestList => _equipmentRequestList;
        public ObservableCollection<DynamicRequestDTOViewModel> EquipmentList => _equipmentList;

        public ICommand BackCommand { get; }

        public GraphicViewModel(EquipmentViewModel equipmentViewModel, MainViewModel mainViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _dynamicEquipmentController = app.DynamicEquipmentController;
            _equipmentRequestList = new List<DynamicRequestDTOViewModel>();
            _equipmentList = new ObservableCollection<DynamicRequestDTOViewModel>();

            ObservableCollection<DynamicEquipmentRequest> requests = _dynamicEquipmentController.GetAllRequests();
            foreach(DynamicEquipmentRequest request in requests)
            {
                _equipmentRequestList.Add(new DynamicRequestDTOViewModel(request));
            }

            foreach(DynamicEquipmentTypeEnum type in Enum.GetValues<DynamicEquipmentTypeEnum>())
            {
                List<DynamicRequestDTOViewModel> oneTypeRequests = _equipmentRequestList.FindAll(p => p.EquipmentType == type.ToString());
                int sumQuantity = 0;
                foreach(DynamicRequestDTOViewModel dynamicRequestDTOViewModel in oneTypeRequests)
                {
                    sumQuantity += dynamicRequestDTOViewModel.Quantity;
                }
                _equipmentList.Add(new DynamicRequestDTOViewModel(new DynamicEquipmentRequest("", sumQuantity, type, "", DateTime.Now)));
            }

            BackCommand = new BackGraphicCommand(equipmentViewModel, mainViewModel);
        }
    }
}

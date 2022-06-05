using HospitalMain.Controller;
using HospitalMain.Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class OrderCommand : CommandBase
    {
        private readonly OrderDynamicEquipmentViewModel _orderDynamicEq;
        private readonly DynamicEquipmentController _dynamicController;
        private readonly MainViewModel _mainViewModel;

        public OrderCommand(OrderDynamicEquipmentViewModel orderDynamicEquipmentViewModel, DynamicEquipmentController dynamicEquipmentController, MainViewModel mainViewModel)
        {
            _orderDynamicEq = orderDynamicEquipmentViewModel;
            _dynamicController = dynamicEquipmentController;
            _mainViewModel = mainViewModel;

            _orderDynamicEq.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_orderDynamicEq.Quantity.ToString()) && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            //generisanje ID-a
            int orderID = _dynamicController.generateID();

            //narucivanje
            _dynamicController.NewOrder(new DynamicEquipmentRequest(orderID.ToString(), _orderDynamicEq.Quantity, _orderDynamicEq.EquipmentType, _orderDynamicEq.ShortDescription, DateTime.Now));

            //navigacija
            if (parameter.ToString() == "NewOrder")
            {
                _mainViewModel.CurrentViewModel = new RequestsViewModel(_mainViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(OrderDynamicEquipmentViewModel.Quantity))
            {
                OnCanExecutedChanged();
            }
        }
    }
}

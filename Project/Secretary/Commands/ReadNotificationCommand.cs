using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class ReadNotificationCommand : CommandBase
    {
        private NotificationViewModel _notificationsViewModel;
        private MainViewModel _mainViewModel;

        public ReadNotificationCommand(MainViewModel mainViewModel, NotificationViewModel notificationViewModel)
        {
            _notificationsViewModel = notificationViewModel;
            _mainViewModel = mainViewModel;

            _notificationsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_notificationsViewModel.SelectedNotification == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _notificationsViewModel.Notifications.Remove(_notificationsViewModel.SelectedNotification);

            if(parameter.ToString() == "Procitaj")
            {
                _mainViewModel.CurrentViewModel = _notificationsViewModel;
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NotificationViewModel.SelectedNotification))
            {
                OnCanExecutedChanged();
            }
        }
    }
}

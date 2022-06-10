using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Controller;
using Utility;

using Admin.Views;

namespace Admin.ViewModel
{
    public class ChooseFormViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private readonly RoomController roomController;
        private String title;

        public String Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public ChooseFormViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);

            var app = Application.Current as App;
            roomController = app.roomController;

            Title = "Choose form for room\n" + roomController.GetClipboardRoom().RoomNb;
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
                case "help":
                    break;
                case "logout":
                    mainWindow.CurrentView = new LoginView();
                    break;
                case "equipmentTransfer":
                    mainWindow.CurrentView = new ScheduleEquipmentTransferView();
                    break;
                case "changeType":
                    break;
                case "renovation":
                    mainWindow.CurrentView = new ScheduleRenovationView();
                    break;
            }
        }
    }
}

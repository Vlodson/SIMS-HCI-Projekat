using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
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
    public class RecordRenovationViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private Renovation renovation;
        private RenovationController renovationController;
        private String title;
        private String renovationType;
        private String parcelling;
        private String startDate;
        private String endDate;

        public String Title
        {
            get { return title; }
            set
            {
                if(title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public String RenovationType
        {
            get { return renovationType; }
            set
            {
                if(renovationType != value)
                {
                    renovationType = value;
                    OnPropertyChanged("RenovationType");
                }
            }
        }

        public String Parcelling
        {
            get { return parcelling; }
            set
            {
                if (parcelling != value)
                {
                    parcelling = value;
                    OnPropertyChanged("Parcelling");
                }
            }
        }

        public String StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public String EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public RecordRenovationViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            SaveCommand = new ICommandTemplate(OnSave);

            var app = Application.Current as App;
            renovationController = app.renovationController;

            renovation = renovationController.ReadAll().Last();

            Title = "Record renovation for room\n" + renovation.OriginRoom.RoomNb;
            RenovationType = renovation.Type.ToString();
            Parcelling = renovation.DestinationRoom is null ? "No parcelling needed" : "Merge with room " + renovation.DestinationRoom.RoomNb;
            StartDate = renovation.StartDate.ToString();
            EndDate = renovation.EndDate.ToString();
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    renovationController.DeleteRenovation(renovation.Id);
                    mainWindow.CurrentView = new ScheduleRenovationView();
                    break;
                case "home":
                    renovationController.DeleteRenovation(renovation.Id);
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    renovationController.DeleteRenovation(renovation.Id);
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "save":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
            }
        }

        public void OnSave()
        {
            renovationController.RecordRenovation(renovation.Id);
            OnNavigation("save");
        }
    }
}

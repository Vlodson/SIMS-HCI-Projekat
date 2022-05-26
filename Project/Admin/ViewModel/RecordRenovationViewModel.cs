using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Admin.View;
using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;

namespace Admin.ViewModel
{
    public class RecordRenovationViewModel
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }

        private Renovation renovation;
        private RenovationController renovationController;

        public String Title;
        public String RenovationType;
        public String Parcelling;
        public String StartDate;
        public String EndDate;

        public RecordRenovationViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            SaveCommand = new ICommandTemplate(OnSave);

            var app = Application.Current as App;
            renovationController = app.renovationController;

            renovation = renovationController.ReadAll().Last();

            Title = "Record renovation for room\n" + renovation.OriginRoom.RoomNb;
            RenovationType = renovation.Type.ToString();
            Parcelling = renovation.DestinationRoom is null ? "No parcelling needed" : "Merge with room" + renovation.DestinationRoom.RoomNb;
            StartDate = renovation.StartDate.ToString();
            EndDate = renovation.EndDate.ToString();
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    break;
                case "home":
                    break;
                case "logout":
                    break;
                case "discard":
                    break;
            }
        }

        public void OnSave()
        {
            renovationController.RecordRenovation(renovation.Id);
            OnNavigation("home");
        }
    }
}

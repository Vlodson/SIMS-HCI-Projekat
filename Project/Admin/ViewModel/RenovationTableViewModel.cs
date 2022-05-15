﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

namespace Admin.ViewModel
{
    public class RenovationTableViewModel: BindableBase
    {
        private RenovationController renovationController;
        public ObservableCollection<FriendlyRenovation> Renovations { get; set; }

        public RenovationTableViewModel()
        {
            var app = Application.Current as App;
            renovationController = app.renovationController;

            ObservableCollection<Renovation> renovations = renovationController.ReadAll();

            Renovations = new ObservableCollection<FriendlyRenovation>();
            foreach (Renovation renovation in renovations)
                Renovations.Add(new FriendlyRenovation(renovation));
        }
    }

    public class FriendlyRenovation
    {
        public int OriginRoom { get; set; }
        public String Type { get; set; }
        public DateOnly EndDate { get; set; }

        public FriendlyRenovation(Renovation renovation)
        {
            OriginRoom = renovation.OriginRoom.RoomNb;
            Type = renovation.Type.ToString();
            EndDate = renovation.EndDate;
        }
    }
}
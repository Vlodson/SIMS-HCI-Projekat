﻿using System;
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
    public class ScheduleRenovationViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RecordCommand { get; private set; }
        public ICommandTemplate SaveCommand { get; private set; }
        public ICommandTemplate FillCommand { get; private set; }
        public ICommandTemplate SelectRoomCommand { get; private set; }
        public ICommandTemplate<String> SplitMergeCommand { get; private set; }

        private RenovationController _renovationController;
        private RoomController _roomController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public ObservableCollection<RenovationTypeEnum> RenovationTypes { get; set; }
        public Room OriginRoom { get; set; }
        public String Title { get; set; }

        private int id;
        private String splitMerge;
        private RenovationTypeEnum selectedRenovationType;
        private String destinationRoomNb;
        private DateTime startDate;
        private DateTime endDate;

        #region Properties
        public String SplitMerge
        {
            get { return splitMerge; }
            set
            {
                if(splitMerge != value)
                {
                    splitMerge = value;
                    OnPropertyChanged("SplitMerge");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RenovationTypeEnum SelectedRenovationType
        {
            get { return selectedRenovationType; }
            set
            {
                if(selectedRenovationType != value)
                {
                    selectedRenovationType = value;
                    OnPropertyChanged("SelectedRenovationType");

                    if (selectedRenovationType != RenovationTypeEnum.Parcelling)
                        SplitMerge = "ordinary";
                }
            }
        }

        public String DestinationRoomNb
        {
            get { return destinationRoomNb; }
            set
            {
                if(destinationRoomNb != value)
                {
                    destinationRoomNb = value;
                    OnPropertyChanged("DestinationRoomNb");
                }
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if(startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if(endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                    RecordCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        public ScheduleRenovationViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RecordCommand = new ICommandTemplate(OnRecord, CanRecordSave);
            SaveCommand = new ICommandTemplate(OnSave, CanRecordSave);
            FillCommand = new ICommandTemplate(OnFill);
            SplitMergeCommand = new ICommandTemplate<String>(OnSplitMerge, CanSplitMerge);

            var app = Application.Current as App;
            _renovationController = app.renovationController;
            _roomController = app.roomController;

            int id = 0;
            if (_renovationController.ReadAll().Count > 0)
                id = _renovationController.ReadAll().Max(reno => int.Parse(reno.Id)) + 1;

            OriginRoom = _roomController.GetClipboardRoom();
            Title = "Schedule renovation for room\n" + OriginRoom.RoomNb;
            RenovationTypes = new ObservableCollection<RenovationTypeEnum>(Enum.GetValues<RenovationTypeEnum>());
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        } 

        public void OnRecord()
        {
            switch (SplitMerge)
            {
                case "ordinary":
                    _renovationController.ScheduleRenovation(
                        id.ToString(),
                        OriginRoom.Id,
                        "",
                        SelectedRenovationType,
                        DateOnly.Parse(StartDate.ToShortDateString()),
                        DateOnly.Parse(EndDate.ToShortDateString())
                        );
                    break;
                case "merge":
                    _renovationController.ScheduleRenovation(
                        id.ToString(),
                        OriginRoom.Id,
                        _roomController.GetSelectedRoom().Id,
                        SelectedRenovationType,
                        DateOnly.Parse(StartDate.ToShortDateString()),
                        DateOnly.Parse(EndDate.ToShortDateString())
                        );

                    _renovationController.MergeRooms(new Renovation(
                        id.ToString(),
                        OriginRoom,
                        _roomController.GetSelectedRoom(),
                        SelectedRenovationType,
                        DateOnly.Parse(StartDate.ToShortDateString()),
                        DateOnly.Parse(EndDate.ToShortDateString())
                        ));
                    break;
                case "split":
                    _renovationController.ScheduleRenovation(
                        id.ToString(),
                        OriginRoom.Id,
                        "",
                        SelectedRenovationType,
                        DateOnly.Parse(StartDate.ToShortDateString()),
                        DateOnly.Parse(EndDate.ToShortDateString())
                        );

                    _renovationController.SplitRoom(new Renovation(
                        id.ToString(),
                        OriginRoom,
                        new Room(),
                        SelectedRenovationType,
                        DateOnly.Parse(StartDate.ToShortDateString()),
                        DateOnly.Parse(EndDate.ToShortDateString())
                        ));
                    break;
            }

            // change view/view model
        }

        public bool CanRecordSave()
        {
            bool can_record = false;
            if (OriginRoom != null)
            {
                    Renovation renovation = new Renovation(
                    "0",
                    OriginRoom,
                    new Room(),
                    SelectedRenovationType,
                    DateOnly.Parse(StartDate.ToShortDateString()),
                    DateOnly.Parse(EndDate.ToShortDateString())
                    );

                can_record = _renovationController.OccupiedAtTheTime(renovation);
            }

            return can_record && !String.IsNullOrEmpty(SplitMerge) && StartDate >= DateTime.Today && EndDate > StartDate;
        } 

        public void OnSave()
        {
            Renovation renovation;
            if (_roomController.GetSelectedRoom() is null)
            {
                renovation = new Renovation(
                    id.ToString(),
                    OriginRoom,
                    new Room(),
                    SelectedRenovationType,
                    DateOnly.Parse(StartDate.ToShortDateString()),
                    DateOnly.Parse(EndDate.ToShortDateString())
                    );
            }
            else
            {
                renovation = new Renovation(
                    id.ToString(),
                    OriginRoom,
                    _roomController.GetSelectedRoom(),
                    SelectedRenovationType,
                    DateOnly.Parse(StartDate.ToShortDateString()),
                    DateOnly.Parse(EndDate.ToShortDateString())
                    );
            }

            _renovationController.SetClipboardRenovation(renovation);
        }

        public void OnFill()
        {
            // TODO
        }

        public void OnSelection()
        {
            mainWindow.Width = 750;
            mainWindow.Height = 430;
            mainWindow.CurrentView = new HospitalLayoutSubmenuView(mainWindow.CurrentView);

            Room DestinationRoom = _roomController.GetSelectedRoom();
            if(DestinationRoom != null)
            {
                if(DestinationRoom.Id == OriginRoom.Id)
                {
                    _roomController.RemoveSelectedRoom();
                    DestinationRoomNb = "Same room selected";
                    return;
                }
                if (DestinationRoom.Occupancy)
                {
                    _roomController.RemoveSelectedRoom();
                    DestinationRoomNb = "Room currently occupied";
                    return;
                }
            }

            DestinationRoomNb = DestinationRoom.RoomNb.ToString();
        }

        public bool CanSplitMerge(String pacrelType)
        {
            return SelectedRenovationType == RenovationTypeEnum.Parcelling;
        }

        public void OnSplitMerge(String parcelType)
        {
            SplitMerge = parcelType;
            if (parcelType == "merge")
                OnSelection();
            else
                DestinationRoomNb = "Room not needed";
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.CurrentView = new ChooseFormView();
                    break;
                case "home":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "discard":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
            }
        }
    }
}

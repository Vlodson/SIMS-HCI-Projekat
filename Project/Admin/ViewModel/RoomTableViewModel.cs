using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class RoomTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private RoomController roomController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        private ObservableCollection<FriendlyRoom> rooms;

        public String Search
        {
            get { return search; }
            set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }
        public ObservableCollection<FriendlyRoom> Rooms
        {
            get { return rooms; }
            set
            {
                if(rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }

        public RoomTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            roomController = app.roomController;

            ObservableCollection<Room> rooms = roomController.ReadAll();

            Rooms = new ObservableCollection<FriendlyRoom>();
            foreach(Room room in rooms)
                Rooms.Add(new FriendlyRoom(room));

            Search = "Enter Query";
        }

        public void OnRemove()
        {
            return;
        }

        public bool CanRemove()
        {
            return false;
        }

        public void OnQuery()
        {
            Rooms.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Room> rooms = roomController.ReadAll();
                foreach (Room roomItem in rooms)
                    Rooms.Add(new FriendlyRoom(roomItem));
                return;
            }

            ObservableCollection<Room> queriedRooms = roomController.QueryRooms(Search);
            foreach (Room room in queriedRooms)
                Rooms.Add(new FriendlyRoom(room));
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
                case "logout":
                    break;
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
                case "medicine":
                    mainWindow.CurrentView = new MedicineTableView();
                    break;
                case "transfers":
                    mainWindow.CurrentView = new EquipmentTransferTableView();
                    break;
                case "renovations":
                    mainWindow.CurrentView = new RenovationTableView();
                    break;
                case "answers":
                    mainWindow.CurrentView = new RatingsView();
                    break;
                case "help":
                    mainWindow.CurrentView = new QueryHelpView(mainWindow.CurrentView);
                    break;
            }
        }

    }

    public class FriendlyRoom
    {
        public String Id { get; set; }
        public int Floor { get; set; }
        public int RoomNb { get; set; }
        public bool Occupancy { get; set; }
        public String Type { get; set; }

        public FriendlyRoom(Room room)
        {
            Id = room.Id;
            Floor = room.Floor;
            RoomNb = room.RoomNb;
            Occupancy = room.Occupancy;
            Type = RoomTypeEnumExtensions.ToFriendlyString(room.Type);
        }
    }
}

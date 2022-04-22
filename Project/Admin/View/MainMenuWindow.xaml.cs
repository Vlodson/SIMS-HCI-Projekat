using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using Model;
using Controller;
using HospitalMain.Enums;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        // clear roomList
        // check which button is pressed and then make roomList equal only those which are on the selceted floor with .Where(r => r.floor == pressed_floor)
        // then clear the children of the stack panels
        // then call create blueprint
        public ObservableCollection<Room> roomList;
        public ObservableCollection<Room> floorRoomList;

        public MainMenuWindow()
        {
            InitializeComponent();
            // wpf is actually comically retarded, so i need to "show" the current window
            // so it can calculate the auto widths and heights even though it fucking rendered them
            this.Show(); 

            var app = Application.Current as App;
            RoomController _roomController = app.roomController;
            this.DataContext = this;

            roomList = new ObservableCollection<Room>();
            floorRoomList = new ObservableCollection<Room>();
            for(int i = 0; i < 20; i++)
            {
                _roomController.CreateRoom(i.ToString(), i%4+1, i, false, RoomTypeEnum.Patient_Room);
            }
            roomList = _roomController.ReadAll();

            makeFloorButtons();

            // always show first floor when opening
            floorRoomList = new ObservableCollection<Room>(roomList.Where(r => r.Floor == 1));
            makeBlueprint();
        }

        private void makeBlueprint()
        {
            int evenRoomNb = floorRoomList.Where(r => r.RoomNb % 2 == 0).Count();
            int oddRoomNb = floorRoomList.Where(r => r.RoomNb % 2 == 1).Count();
            var s = "";
            foreach (Room r in floorRoomList)
            {
                s += r.Id;
                Border room = new Border();
                room.BorderBrush = Brushes.Black;
                room.BorderThickness = new Thickness(1);
                room.Background = Brushes.Transparent;

                TextBlock roomId = new TextBlock();
                roomId.Text = r.Id + " " + r.Type.ToString();
                roomId.Margin = new Thickness(5,0,5,0);
                roomId.TextWrapping = TextWrapping.Wrap;
                roomId.HorizontalAlignment = HorizontalAlignment.Center;
                roomId.VerticalAlignment = VerticalAlignment.Center;
                room.Child = roomId;

                if (r.RoomNb % 2 == 0)
                {
                    room.Width = Hall.ActualWidth / evenRoomNb;
                    upperRooms.Children.Add(room);
                }
                else
                {
                    room.Width = Hall.ActualWidth / oddRoomNb;
                    lowerRooms.Children.Add(room);
                }
            }

        }

        private void makeFloorButtons()
        {
            int floors = roomList.Max(r => r.Floor); // what a weird way to find maximum values, but also powerful
            for(int i = 1; i < floors+1; i++)
            {
                Button floorBtn = new Button();
                floorBtn.Content = "Floor " + i;
                floorBtn.Click += (s, e) =>
                {
                    // clean what you currently have drawn
                    upperRooms.Children.Clear();
                    lowerRooms.Children.Clear();

                    Button pressedFloorNb = (Button)s;
                    int floorNb = int.Parse(Regex.Match(pressedFloorNb.Content.ToString(), @"\d+").Value);

                    floorRoomList = new ObservableCollection<Room>(roomList.Where(r => r.Floor == floorNb));
                    
                    makeBlueprint();
                };

                floorButtons.Children.Add(floorBtn);
            }
        }

        private void Hall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pritiso u hodniku");
        }
    }
}

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
using System.IO;

using Model;
using Controller;
using Utility;
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
        private RoomController _roomController;

        public MainMenuWindow()
        {
            InitializeComponent();
            // wpf is actually comically retarded, so i need to "show" the current window
            // so it can calculate the auto widths and heights even though it fucking rendered them
            this.Show(); 

            var app = Application.Current as App;
            _roomController = app.roomController;
            this.DataContext = this;

            roomList = new ObservableCollection<Room>();
            floorRoomList = new ObservableCollection<Room>();

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
            // similar to how you did the button onclick do the same with room click, also create a room_repo clipboard room
            foreach (Room r in floorRoomList)
            {
                Border room = new Border();
                room.BorderBrush = Brushes.Black;
                room.BorderThickness = new Thickness(1);
                room.Background = Brushes.Transparent;
                room.MouseDown += (s, e) =>
                {
                    _roomController.SetClipboardRoom(r);
                    this.Hide();
                    ChooseFormWindow formWindow = new ChooseFormWindow();
                    formWindow.Owner = this;
                    formWindow.Show();
                };

                TextBlock roomId = new TextBlock();
                roomId.Text = r.RoomNb + " " + r.Type.ToString();
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

                    // parse which floor is pressed, because it didnt work with i :)
                    Button pressedFloorNb = (Button)s;
                    int floorNb = int.Parse(Regex.Match(pressedFloorNb.Content.ToString(), @"\d+").Value);

                    floorRoomList = new ObservableCollection<Room>(roomList.Where(r => r.Floor == floorNb));
                    
                    makeBlueprint();
                };

                floorButtons.Children.Add(floorBtn);
            }
        }

        private void tableViewBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TableWindow tableWindow = new TableWindow();
            tableWindow.Hide();
            tableWindow.ShowDialog();
            this.Show();
        }
    }
}

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
    /// Interaction logic for HospitalLayoutSubmenuWindow.xaml
    /// </summary>
    public partial class HospitalLayoutSubmenuWindow : Window
    {
        public ObservableCollection<Room> roomList;
        public ObservableCollection<Room> floorRoomList;
        private RoomController _roomController;

        public HospitalLayoutSubmenuWindow()
        {
            InitializeComponent();
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
            foreach (Room r in floorRoomList)
            {
                Border room = new Border();
                room.BorderBrush = Brushes.Black;
                room.BorderThickness = new Thickness(1);
                room.Background = (Brush)new BrushConverter().ConvertFrom("#ececec");
                room.MouseDown += (s, e) =>
                {
                    _roomController.SetSelectedRoom(r);
                    this.Close();
                };

                TextBlock roomId = new TextBlock();
                roomId.Text = r.RoomNb + " " + RoomTypeEnumExtensions.ToFriendlyString(r.Type);
                roomId.Margin = new Thickness(5, 0, 5, 0);
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
            int floors = roomList.Max(r => r.Floor);
            for (int i = 1; i < floors + 1; i++)
            {
                Button floorBtn = new Button();
                floorBtn.Content = "Floor " + i;
                floorBtn.Padding = new Thickness(5, 0, 5, 0);
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

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

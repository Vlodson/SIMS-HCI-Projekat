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
        ObservableCollection<Room> roomList;

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
            for(int i = 0; i < 20; i++)
            {
                roomList.Add(new Room(i.ToString(), 1, i, false, RoomTypeEnum.Patient_Room));
            }

            makeBlueprint();
        }

        private void makeBlueprint()
        {
            int evenRoomNb = roomList.Where(r => r.RoomNb % 2 == 0).Count();
            int oddRoomNb = roomList.Where(r => r.RoomNb % 2 == 1).Count();

            foreach (Room r in roomList)
            {
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

        private void Hall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pritiso u hodniku");
        }
    }
}

using System;
using System.Collections.Generic;
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

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for ChooseFormWindow.xaml
    /// </summary>
    public partial class ChooseFormWindow : Window
    {
        public Room selectedRoom { get; set; }

        public ChooseFormWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            RoomController _roomController = app.roomController;

            selectedRoom = _roomController.GetClipboardRoom();

            titleTextBlock.Text = "Choose form for room\n" + selectedRoom.RoomNb;
        }

        private void transferEquipmentBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ScheduleEquipmentTransferWindow scheduleEquipmentTransferWindow = new ScheduleEquipmentTransferWindow();
            scheduleEquipmentTransferWindow.Owner = this;
            scheduleEquipmentTransferWindow.Show();
        }

        private void changeRoomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Implementation not needed for CP3");
        }

        private void renovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ScheduleRenovationWindow scheduleRenovationWindow = new ScheduleRenovationWindow();
            scheduleRenovationWindow.Owner = this;
            scheduleRenovationWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }
    }
}

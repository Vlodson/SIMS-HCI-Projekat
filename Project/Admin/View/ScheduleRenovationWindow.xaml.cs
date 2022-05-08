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
using HospitalMain.Enums;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationWindow.xaml
    /// </summary>
    public partial class ScheduleRenovationWindow : Window
    {
        public Room OriginRoom { get; set; }
        public RenovationTypeEnum RenovationType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        private RenovationController _renovationController;
        private RoomController _roomController;

        public ScheduleRenovationWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _renovationController = app.renovationController;
            _roomController = app.roomController;


            OriginRoom = _roomController.GetClipboardRoom();
            Title.Text = "Schedule Renovation for room\n" + OriginRoom.RoomNb;

            renoTypeComboBox.Items.Clear();
            renoTypeComboBox.ItemsSource = Enum.GetValues<RenovationTypeEnum>();
        }

        private void parcellingRoomTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void splitBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Execute_Record(object sender, ExecutedRoutedEventArgs e)
        {
            int id = 0;
            if (_renovationController.ReadAll().Count > 0)
                id = _renovationController.ReadAll().Max(reno => int.Parse(reno.Id)) + 1;
            RenovationType = (RenovationTypeEnum)renoTypeComboBox.SelectedItem;
            StartDate = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            EndDate = DateOnly.FromDateTime(endDate.SelectedDate.Value);

            _renovationController.ScheduleRenovation(id.ToString(), OriginRoom.Id, RenovationType, StartDate, EndDate);
            this.Close();
            RecordRenovationWindow recordRenovationWindow = new RecordRenovationWindow();
            recordRenovationWindow.Owner = App.Current.MainWindow;
            recordRenovationWindow.Show();
        }

        private void CanExecute_Record(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(renoTypeComboBox.SelectedItem is null || startDate.SelectedDate is null || endDate.SelectedDate is null || endDate.SelectedDate < startDate.SelectedDate);
        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            int id = 0;
            if (_renovationController.ReadAll().Count > 0)
                id = _renovationController.ReadAll().Max(reno => int.Parse(reno.Id)) + 1;
            RenovationType = (RenovationTypeEnum)renoTypeComboBox.SelectedItem;
            StartDate = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            EndDate = DateOnly.FromDateTime(endDate.SelectedDate.Value);

            _renovationController.SetClipboardRenovation(new Renovation(id.ToString(), OriginRoom, RenovationType, StartDate, EndDate));
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(renoTypeComboBox.SelectedItem is null || startDate.SelectedDate is null || endDate.SelectedDate is null || endDate.SelectedDate < startDate.SelectedDate);
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _roomController.RemoveSelectedRoom();
            this.Close();
            this.Owner.Show();
        }
    }
}

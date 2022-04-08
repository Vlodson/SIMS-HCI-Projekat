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

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        public int room_nb { get; set; }
        public int floor_nb { get; set; }
        public string room_type { get; set; }
        public bool occupied { get; set; }

        public EditRoomWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            occupied = false;
        }

        private void editRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            string message = room_nb.ToString() + "\n" + floor_nb.ToString() + "\n" + roomType + "\n" + occupied.ToString();
            MessageBox.Show(message);
        }

        private void OccRBtn_Checked(object sender, RoutedEventArgs e)
        {
            occupied = true;
        }
    }
}

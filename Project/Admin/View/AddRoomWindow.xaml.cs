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
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public int room_nb { get; set; }
        public int floor_nb { get; set; }
        public string room_type { get; set; }

        public AddRoomWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            string message = room_nb.ToString() + "\n" + floor_nb.ToString() + "\n" + room_type;
            MessageBox.Show(message);
        }
    }

}

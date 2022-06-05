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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

using Admin.ViewModel;
using Utility;

namespace Admin.Views
{
    /// <summary>
    /// Interaction logic for QueryHelpView.xaml
    /// </summary>
    public partial class QueryHelpView : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ICommandTemplate<String> NavigationCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private UserControl caller;

        private String helpText;
        public String HelpText
        {
            get { return helpText; }
            set
            {
                if (value != helpText)
                {
                    helpText = value;
                    OnPropertyChanged("HelpText");
                }
            }
        }

        public QueryHelpView(UserControl caller)
        {
            InitializeComponent();
            this.DataContext = this;

            NavigationCommand = new ICommandTemplate<String>(OnNavigation);

            this.caller = caller;
            HelpText = "Welcome to the query helper.\n\n" +
                "General instruction form:\n" +
                "Variable: <operation> value1 [value2]\n\n" +
                "Available operations:\n" +
                "<to> - given two values returns all elements whose given variable value between the two values\n" +
                "<eq> - given one value returns all elements whose given variable value equal to that values\n" +
                "<gt> - given one value returns all elements whose given variable value greater than value\n" +
                "<lt> - given one value returns all elements whose given variable value less than value\n" +
                "<ge> - given one value returns all elements whose given variable value greater than or eqal to value\n" +
                "<le> - given one value returns all elements whose given variable value less than or eqal to value\n" +
                "blank query - resets the table to initial state\n\n" +
                "Example with one value:\n" +
                "RoomNb: <eq> 5\n" +
                "Example with two values\n" +
                "RoomNb: <to> 5 10\n\n" +
                "Avaliable commands for " + CallerExtension();

        }

        public String CallerExtension()
        {
            switch (caller.GetType().Name)
            {
                case "EquipmentTableView":
                    return "equipment table:\n" +
                    "Id, RoomId";
                case "EquipmentTransferTableView":
                    return "equipment transfer table:\n" +
                        "None";
                case "MedicineTableView":
                    return "medicine table:\n" +
                        "Name";
                case "RenovationTableView":
                    return "renovation table:\n None";
                case "RoomTableView":
                    return "room table:\n" +
                        "RoomNb, Floor, Occupancy";
                default:
                    return "";
            }
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "logout":
                    break;
                default:
                    mainWindow.CurrentView = this.caller;
                    break;
            }
        }
    }
}

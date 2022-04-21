using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private TherapyRepo _therapyRepo;
        private TherapyController _therapyController;
        private string patientBind;
        private DateTime dateBind;
        public static ObservableCollection<Therapy> therapyBind
        {
            get;
            set;
        }
        
        public ReportPage(Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PatientBind = exam.PatientId;
            this.DateBind = exam.Date;

            App app = Application.Current as App;
            _therapyRepo = app.TherapyRepo;
            _therapyController = app.therapyController;

            if (File.Exists(_therapyRepo.dbPath))
                _therapyRepo.LoadTherapy();
            therapyBind = _therapyController.findById(exam.Id);
        }

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Unesi izvestaj sa pregleda...")
                txtBox.Text = string.Empty;
        }
    }
}

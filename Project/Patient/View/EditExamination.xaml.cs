using Controller;
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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for EditExamination.xaml
    /// </summary>
    public partial class EditExamination : Window
    {
        private DoctorController _doctorController;
        private ExamController _examController;
        public EditExamination()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;

            ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(MainWindow.selected.doctor);
            //AddExamination
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime newDate = (DateTime)ExamsAvailable.SelectedItem;
            _examController.PatientEditExam(MainWindow.selected, newDate);
            this.Close();
        }
    }
}

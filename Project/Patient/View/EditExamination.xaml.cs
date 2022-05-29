using Controller;
using Model;
using Repository;
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
        private RoomController _roomController;
        private PatientController _patientController;
        //private ExaminationRepo _examinationRepo;

        public EditExamination()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _roomController = app.RoomController;
            _patientController = app.PatientController;

            ExamsAvailable.ItemsSource = _doctorController.AvailableMoveExaminations(ExaminationsList.selected);
            Odeljenje.Content = ExaminationsList.selected.DoctorType;
            Lekar.Content = ExaminationsList.selected.DoctorNameSurname;
            StariTermin.Content = ExaminationsList.selected.Date;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Examination newExamination = (Examination)ExamsAvailable.SelectedItem;
            DateTime newDate = newExamination.Date;

            _examController.PatientEditExamForMoving(ExaminationsList.selected, newDate);
            _examController.SaveExaminationRepo();
            
            
            this.Close();
        }
    }
}

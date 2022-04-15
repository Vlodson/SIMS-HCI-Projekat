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
        private ExaminationRepo _examinationRepo;

        public EditExamination()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _examinationRepo = app.ExaminationRepo;

            ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(ListExaminations.selected.Doctor);
            //AddExamination
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Examination newExamination = (Examination)ExamsAvailable.SelectedItem;
            DateTime newDate = newExamination.Date;
            _examController.PatientEditExam(ListExaminations.selected, newDate);
            _examinationRepo.SaveExamination();
            this.Close();
        }
    }
}

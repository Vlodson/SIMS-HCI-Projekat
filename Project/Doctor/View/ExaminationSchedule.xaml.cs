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
    /// Interaction logic for ExaminationSchedule.xaml
    /// </summary>
    public partial class ExaminationSchedule : Page
    {
        private ExamController _examController;
        private ExaminationRepo _examRepo;
        public static Examination SelectedItem
        {
            get;
            set;
        }
        public static ObservableCollection<Examination> Examinations { get; set; }
        public ExaminationSchedule()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _examController = app.examController;
            _examRepo = app.examRepo;

            if (File.Exists(_examRepo.DBPath))
                _examRepo.LoadExamination();

            Examinations = _examController.ReadDoctorExams(MainWindow._uid);
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination(this);
            NavigationService.Navigate(addExamination);
            _examRepo.SaveExamination();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = (Examination)dataGridExaminations.SelectedItem;
            UpdateExamination updateExamination = new UpdateExamination(SelectedItem, this);
            NavigationService.Navigate(updateExamination);
            _examRepo.SaveExamination();
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Examination selectedItem = (Examination)dataGridExaminations.SelectedItem;
            if (selectedItem != null)
            {
                _examController.DoctorRemoveExam(selectedItem);
                dataGridExaminations.ItemsSource = _examController.ReadDoctorExams(MainWindow._uid);
                _examRepo.SaveExamination();
            }

        }
    }
}

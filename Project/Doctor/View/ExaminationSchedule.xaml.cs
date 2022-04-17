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
using System.Windows.Shapes;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for ExaminationSchedule.xaml
    /// </summary>
    public partial class ExaminationSchedule : Window
    {
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        public static Examination SelectedItem
        {
            get;
            set;
        }

        public static ObservableCollection<Examination> Examinations { get; set; }

        //public static ObservableCollection<Examination> Examinations
        //{
        //    get;
        //    set;
        //}

        public ExaminationSchedule()
        {
            InitializeComponent();
            this.DataContext = this;

            string idDoc = "IDDOC";

            App app = Application.Current as App;
            _examRepo = app.ExaminationRepo;
            if (File.Exists(_examRepo.dbPath))
                _examRepo.LoadExamination();

            _examController = app.ExamController;
            Examinations = _examController.ReadDoctorExams(idDoc);
            
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination();
            this.Close();
            addExamination.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = (Examination)dataGridExaminations.SelectedItem;
            UpdateExamination updateExamination = new UpdateExamination(SelectedItem);
            this.Close();
            updateExamination.Show();
        }


        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Examination selectedItem = (Examination)dataGridExaminations.SelectedItem;
            if (selectedItem != null)
            {
                _examController.DoctorRemoveExam(selectedItem);
                dataGridExaminations.ItemsSource = _examController.ReadDoctorExams("IDDOC");
            }
            _examRepo.SaveExamination();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _examRepo.SaveExamination();
        }

    }
}

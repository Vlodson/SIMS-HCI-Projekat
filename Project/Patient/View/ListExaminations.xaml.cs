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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for ListExaminations.xaml
    /// </summary>
    /// 
    

    public partial class ListExaminations : Window
    {
        private ExamController _examinationController;
        private ExaminationRepo _examinationRepo;
        public static Examination selected;

        public static ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }
        public ListExaminations()
        {

            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examinationRepo = app.ExaminationRepo;
            if (File.Exists(_examinationRepo.dbPath))
                _examinationRepo.LoadExamination();
            _examinationController = app.ExamController;
            Examinations = _examinationController.ReadPatientExams("2");
        }
        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination();
            addExamination.Show();
        }

        private void EditExamination_Click(object sender, RoutedEventArgs e)
        {
            selected = (Examination)dataGridExaminations.SelectedItem;

            if (selected != null)
            {
                if (selected.Date.CompareTo(DateTime.Now.AddDays(1)) > 0)
                {
                    EditExamination editExamination = new EditExamination();
                    editExamination.Show();
                }
                    
            }

        }

        private void RemoveExamination_Click(object sender, RoutedEventArgs e)
        {
            _examinationController.RemoveExam((Examination)dataGridExaminations.SelectedItem);
            _examinationRepo.SaveExamination();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _examinationRepo.SaveExamination();
        }
    }
}

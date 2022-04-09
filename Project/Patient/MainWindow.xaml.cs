using Controller;
using Model;
using Patient.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Patient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ExamController _examinationController;
        
        public static ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examinationController = app.ExamController;
            Examinations = _examinationController.ReadPatientExams("idPatient1");
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination();
            addExamination.Show();
        }

        private void EditExamination_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveExamination_Click(object sender, RoutedEventArgs e)
        {
            _examinationController.RemoveExam((Examination)dataGridExaminations.SelectedItem);
        }
    }
}

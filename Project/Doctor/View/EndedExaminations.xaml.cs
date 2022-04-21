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
    /// Interaction logic for EndedExaminations.xaml
    /// </summary>
    public partial class EndedExaminations : Page
    {
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        public static ObservableCollection<Examination> examinations
        {
            get;
            set;
        }

        public EndedExaminations()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _examRepo = app.ExaminationRepo;
            _examController = app.ExamController;

            if (File.Exists(_examRepo.dbPath))
                _examRepo.LoadExamination();
            //examinations = _examRepo.GetAll();
            examinations = _examController.ReadEndedExams();
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            Examination selected = (Examination)dataGridExams.SelectedItem;
            ReportPage reportPage = new ReportPage(selected);
            NavigationService.Navigate(reportPage);
        }
    }
}

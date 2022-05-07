using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for ExaminationsList.xaml
    /// </summary>
    public partial class ExaminationsList : Page
    {
        private ExamController _examinationController;
        //private ExaminationRepo _examinationRepo;
        private DoctorController _doctorController;
        public static Examination selected;

        public static ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }

        public static List<Examination> ExaminationsForDate
        {
            get;
            set;
        }

        public static List<DateOnly> DatesExaminations
        {
            get;
            set;
        }

        public ExaminationsList()
        {
            InitializeComponent();
            this.DataContext = this;
            
            App app = Application.Current as App;
           //_examinationRepo = app.ExaminationRepo;
            _examinationController = app.ExamController;
            _doctorController = app.DoctorController;

            //if (File.Exists(_examinationRepo.dbPath))
            //    _examinationRepo.LoadExamination();


            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            foreach(Examination exam in examinations)
            {
                exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
            }
            Examinations = examinations;

            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            DatesExaminations = new List<DateOnly>();
            foreach(Examination exam in examinations)
            {
                if(exam.Date.Date == today.Date)
                {
                    if(exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                    
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;
            
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            Message.Visibility = Visibility.Hidden;
            AddExamination addExamination = new AddExamination();
            addExamination.ShowDialog();
            //dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == today.Date)
                {
                    if (exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;
            
        }

        private void EditExamination_Click(object sender, RoutedEventArgs e)
        {
            selected = (Examination)dataGridExaminations.SelectedItem;

            if (selected != null)
            {
                if (selected.Date.CompareTo(DateTime.Now) >= 0)
                {
                    Message.Visibility = Visibility.Hidden;
                    EditExamination editExamination = new EditExamination();
                    editExamination.ShowDialog();
                }
                else
                {
                    Message.Content = "Ne mozete pomeriti danasnji termin";
                    Message.Visibility = Visibility.Visible;
                }

            }
            else
            {
                Message.Content = "Morate izabrati termin za pomeranje";
                Message.Visibility = Visibility.Visible;
            }
            //dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == today.Date)
                {
                    if (exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;
        }

        private void RemoveExamination_Click(object sender, RoutedEventArgs e)
        {
            selected = (Examination)dataGridExaminations.SelectedItem;

            if (selected != null)
            {
                if (selected.Date.CompareTo(DateTime.Now) >= 0)
                {
                    Message.Visibility = Visibility.Hidden;
                    _examinationController.RemoveExam((Examination)dataGridExaminations.SelectedItem);
                    //_examinationRepo.SaveExamination();
                    _examinationController.SaveExaminationRepo();
                    dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
                }
                else
                {
                    Message.Content = "Ne mozete otkazati danasnji termin";
                    Message.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Message.Content = "Morate izabrati termin za otkazivanje";
                Message.Visibility = Visibility.Visible;
            }
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == today.Date)
                {
                    if (exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //_examinationRepo.SaveExamination();
            _examinationController.SaveExaminationRepo();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).Content = new PatientMenu();
            this.Visibility = Visibility.Collapsed;
        }

        private void ChangeSelected(object sender, SelectionChangedEventArgs e)
        {
            DateTime selected = (DateTime)Calendar.SelectedDate;
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == selected.Date)
                {
                    if (exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;
        }
        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (DatesExaminations.Contains(DateOnly.FromDateTime(date)))
                button.Background = Brushes.LightBlue;
            else
                button.Background = Brushes.White;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }
    }
   
}

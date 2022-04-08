using ClassDijagramV1._0.Controller;
using Model;
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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.View.Patient
{
    /// <summary>
    /// Interaction logic for AddExaminationPatient.xaml
    /// </summary>
    public partial class AddExaminationPatient : Window
    {

        private DoctorController _doctorController;
        private Doctor _sdoctor;
        public Doctor SDoctor
        {
            get { return _sdoctor; }
            set { _sdoctor = value; }
        }

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }

        public AddExaminationPatient()
        {
            InitializeComponent();
            this.DataContext = this;

            //DoctorsObs = new ObservableCollection<Doctor>();

            var app = Application.Current as App;
            _doctorController = app.DoctorController;
            DoctorsObs = new ObservableCollection<Doctor>(FindAllDoctors());


        }

        private IList<Doctor> FindAllDoctors()
        {
            return _doctorController.GetAll()
                .ToList();
        }
    }
}

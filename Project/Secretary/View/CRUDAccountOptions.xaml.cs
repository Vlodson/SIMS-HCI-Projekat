using System;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;
using System.IO;
using HospitalMain.Enums;
using Secretary.ViewModel;

namespace Secretary
{
    /// <summary>
    /// Interaction logic for CRUDAccountOptions.xaml
    /// </summary>
    public partial class CRUDAccountOptions : Window
    {
        //private PatientRepo patientRepo;

        public CRUDAccountOptions()
        {
            InitializeComponent();
            this.DataContext = new CRUDAccountOptionsViewModel();

            //App app = Application.Current as App;
            //patientRepo = app.PatientRepo;

        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    patientRepo.SavePatient();
        //}
    }
}

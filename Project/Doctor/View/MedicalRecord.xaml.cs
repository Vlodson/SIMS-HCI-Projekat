﻿using Controller;
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
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecord : Page
    {
        private ReportRepo _reportRepo;
        private ReportController _reportController;
        public static ObservableCollection<Report> reports { get; set; }
        public Report _selectedReport { get; set; } 
        public MedicalRecord(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            

            App app = Application.Current as App;
            _reportRepo = app.reportRepo;
            _reportController = app.reportController;

            if (File.Exists(_reportRepo.dbPath))
                _reportRepo.LoadReport();

            reports = _reportController.findByPatientId(patient.ID);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _selectedReport = (Report)dataGridReports.SelectedItem;
            _selectedReport.Description = txtDescription.Text;
            NavigationService.Refresh();
        }
    }
}
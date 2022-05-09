using System;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;
using System.IO;
using HospitalMain.Enums;
using System.Collections.Generic;
using System.Linq;
using HospitalMain.Model;
using Secretary.ComboBoxTemplate;
using Secretary.ViewModel;

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for CRUDMedicalRecord.xaml
    /// </summary>
    public partial class CRUDMedicalRecord
    {

        public CRUDMedicalRecord()
        {
            InitializeComponent();
            //this.DataContext = new CRUDMedicalRecordViewModel();
        
        }

    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface IReportRepo
    {
        public bool LoadReport();
        public bool SaveReport();
        public void NewReport(Report report);
        public ObservableCollection<Report> findByPatientId(string id);
    }
}

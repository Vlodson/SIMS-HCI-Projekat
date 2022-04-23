using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ReportController
    {

        private ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public void NewReport(Report report)
        {
            _reportService.NewReport(report);
        }

        public ObservableCollection<Report> findByPatientId(string id)
        {
            return _reportService.findByPatientId(id);
        }
    }
}

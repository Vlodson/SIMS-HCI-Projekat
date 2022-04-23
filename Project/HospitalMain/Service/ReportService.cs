using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReportService
    {
        private readonly ReportRepo _reportRepo;

        public ReportService(ReportRepo reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public void NewReport(Report report)
        {
            _reportRepo.NewReport(report);
        }

        public ObservableCollection<Report> findByPatientId(string id)
        {
            return _reportRepo.findByPatientId(id);
        }
    }
}

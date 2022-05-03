using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Service;
using Model;
using HospitalMain.Enums;

namespace Controller
{
    public class RenovationController
    {
        private readonly RenovationService _renovationService;

        public RenovationController(RenovationService renovationService)
        {
            _renovationService = renovationService;
        }

        public bool ScheduleRenovation(String id, String originRoomId, RenovationTypeEnum type, DateOnly startDate, DateOnly endDate)
        {
            return _renovationService.ScheduleRenovation(id, originRoomId, type, startDate, endDate);
        }

        public bool RecordRenovation(String renovationId, String signature)
        {
            return _renovationService.RecordRenovation(renovationId, signature);
        }

        public bool DeleteRenovation(String renovationId)
        {
            return _renovationService.DeleteRenovation(renovationId);
        }

        public bool SetClipboardRenovation(Renovation renovation)
        {
            return _renovationService.SetClipboardRenovation(renovation);
        }

        public Renovation GetClipboardRenovation()
        {
            return _renovationService.GetClipboardRenovation();
        }

        public ObservableCollection<Renovation> ReadAll()
        {
            return _renovationService.ReadAll();
        }

        public bool LoadRenovation()
        {
            return _renovationService.LoadRenovation();
        }

        public bool SaveRenovation()
        {
            return _renovationService.SaveRenovation();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository;
using Model;
using HospitalMain.Enums;

namespace Service
{
    public class RenovationService
    {
        private readonly RenovationRepo _renovationRepo;
        private readonly RoomRepo _roomRepo;
        private readonly ExaminationRepo _examinationRepo;

        public RenovationService(RenovationRepo renovationRepo, RoomRepo roomRepo, ExaminationRepo examinationRepo)
        {
            _renovationRepo = renovationRepo;
            _roomRepo = roomRepo;
            _examinationRepo = examinationRepo;
        }

        public bool ScheduleRenovation(String id, String originRoomId, RenovationTypeEnum type, DateOnly startDate, DateOnly endDate)
        {
            Room originRoom = _roomRepo.GetRoom(originRoomId);
            Renovation renovation = new Renovation(id, originRoom, type, startDate, endDate);
            _renovationRepo.NewRenovation(renovation);
            return true;
        }

        public bool RecordRenovation(String renovationId)
        {
            Renovation renovation = _renovationRepo.GetRenovation(renovationId);
            Room originRoom = renovation.OriginRoom;
            
            _roomRepo.SetRoom(originRoom.Id, originRoom.Equipment, originRoom.Floor, originRoom.RoomNb, originRoom.Occupancy, RoomTypeEnum.Inoperative);
            _renovationRepo.SetRenovation(renovation.Id, renovation.OriginRoom, renovation.Type, renovation.StartDate, renovation.EndDate);
            
            return true;
        }

        public bool OccupiedAtTheTime(Renovation renovation)
        {
            foreach (Examination examination in _examinationRepo.examinationList)
            {
                if (renovation.OriginRoom.Id == examination.ExamRoomId) // destination room missing here. to add after merge/split
                    if (renovation.StartDate >= DateOnly.Parse(examination.Date.ToString()) && renovation.EndDate <= DateOnly.Parse(examination.Date.AddMinutes(examination.Duration).ToString()))
                        return false;

            }

            return true;
        }

        public bool DeleteRenovation(String renovationId)
        {
            return _renovationRepo.DeleteRenovation(renovationId);
        }

        public bool SetClipboardRenovation(Renovation renovation)
        {
            return _renovationRepo.SetClipboardRenovation(renovation);
        }

        public Renovation GetClipboardRenovation()
        {
            return _renovationRepo.GetClipboardRenovation();
        }

        public ObservableCollection<Renovation> ReadAll()
        {
            return _renovationRepo.Renovations;
        }

        public bool LoadRenovation()
        {
            return _renovationRepo.LoadRenovation();
        }

        public bool SaveRenovation()
        {
            return _renovationRepo.SaveRenovation();
        }
    }
}

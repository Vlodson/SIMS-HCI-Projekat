using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


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
            
            _roomRepo.SetRoom(originRoom.Id, originRoom.Equipment, originRoom.Floor, originRoom.RoomNb, originRoom.Occupancy, RoomTypeEnum.Inoperative, originRoom.PreviousType);
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

        public void FinishRenovation(object sender)
        {
            foreach(Renovation renovation in _renovationRepo.Renovations)
            {
                if(renovation.EndDate >= DateOnly.Parse(DateTime.Now.ToString()))
                    _roomRepo.SetRoom(renovation.OriginRoom.Id, renovation.OriginRoom.Equipment, renovation.OriginRoom.Floor, renovation.OriginRoom.RoomNb, renovation.OriginRoom.Occupancy, renovation.OriginRoom.PreviousType, renovation.OriginRoom.PreviousType)
            }
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

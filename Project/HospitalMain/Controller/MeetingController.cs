using HospitalMain.Model;
using HospitalMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class MeetingController
    {
        private MeetingsService _meetingsService;

        public MeetingController(MeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        public bool BookNewMeeting(Meeting meeting)
        {
            return _meetingsService.BookNewMeeting(meeting);
        }

        public void EditMeeting(Meeting meeting)
        {
            _meetingsService.EditMeeting(meeting);
        }

        public bool DeleteMeeting(String meetingID)
        {
            return _meetingsService.DeleteMeeting(meetingID);
        }
    }
}

using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HospitalMain.Repository
{
    public class MeetingsRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<Meeting> MeetingsList { get; set; }

        public MeetingsRepo(string DBPath)
        {
            this.DBPath = DBPath;
            MeetingsList = new ObservableCollection<Meeting>();

            //SaveMeetings();
            //if (File.Exists(DBPath))
            //    LoadMeetings();
        }

        public bool BookNewMeeting(Meeting newMeeting)
        {
            foreach (Meeting meeting in MeetingsList)
            {
                if (meeting.ID.Equals(newMeeting.ID))
                {
                    return false;
                }
            }
            MeetingsList.Add(newMeeting);
            SaveMeetings();
            return true;
        }

        public void EditMeeting(Meeting meeting)
        {
            foreach(Meeting _meeting in MeetingsList)
            {
                if (_meeting.ID.Equals(meeting.ID))
                {
                    _meeting.DateTime = meeting.DateTime;
                    _meeting.RoomID = meeting.RoomID;
                    _meeting.MeetingTopic = meeting.MeetingTopic;
                    _meeting.Doctors = meeting.Doctors;
                    break;
                }
            }
            SaveMeetings();
        }

        public bool DeleteMeeting(String meetingID)
        {
            foreach(Meeting meeting in MeetingsList)
            {
                if (meeting.ID.Equals(meetingID))
                {
                    MeetingsList.Remove(meeting);
                    SaveMeetings();
                    return true;
                }
            }
            return false;
        }

        public void LoadMeetings()
        {
            using FileStream stream = File.OpenRead(DBPath);
            MeetingsList = JsonSerializer.Deserialize<ObservableCollection<Meeting>>(stream);
        }

        public void SaveMeetings()
        {
            string jsonString = JsonSerializer.Serialize(MeetingsList);
            File.WriteAllText(DBPath, jsonString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Notification
    {
        private String content;
        private Boolean isRead;
        private DateTime dateTimeNotification;

        public String Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
        public Boolean IsRead
        {
            get
            {
                return isRead;
            }
            set
            {
                isRead = value;
            }
        }
        public DateTime DateTimeNotification
        {
            get
            {
                return dateTimeNotification;
            }
            set
            {
                dateTimeNotification = value;
            }
        }

        public Notification(string content, bool isRead, DateTime dateTimeNotification)
        {
            Content = content;
            IsRead = isRead;
            DateTimeNotification = dateTimeNotification;
        }
    }
}

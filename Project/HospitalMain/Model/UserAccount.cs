using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalMain.Enums;

namespace HospitalMain.Model
{
    public class UserAccount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _username;
        private String _password;
        private UserType _type;

        public UserAccount() { }

        public UserAccount(String username, String password, UserType type)
        {
            _username = username;
            _password = password;
            _type = type;
        }

        public UserAccount(UserAccount acc)
        {
            this._username = acc.UserName;
            this._password = acc.Password;
            this._type = acc.Type;
        }

        public String UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(UserName)); }
        }

        public String Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public UserType Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }
    }
}

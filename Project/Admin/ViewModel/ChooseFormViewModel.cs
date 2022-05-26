using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Controller;
using Utility;

namespace Admin.ViewModel
{
    public class ChooseFormViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }

        private readonly RoomController roomController;
        public String Title;

        public ChooseFormViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);

            var app = Application.Current as App;
            roomController = app.roomController;

            Title = "Choose form for room\n" + roomController.GetClipboardRoom().RoomNb;
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    break;
                case "help":
                    break;
                case "logout":
                    break;
                case "equipmentTransfer":
                    break;
                case "changeType":
                    break;
                case "renovation":
                    break;
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using HospitalMain.Model;

using Admin.Views;

namespace Admin.ViewModel
{
    public class RatingsViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private AnswerController answerController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        public String Search
        {
            get { return search; }
            set
            {
                if (search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        private ObservableCollection<FriendlyAnswer> answers;
        public ObservableCollection<FriendlyAnswer> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }
        private FriendlyEquipment selectedAnswer;
        public FriendlyEquipment SelectedAnswer
        {
            get { return selectedAnswer; }
            set
            {
                if (selectedAnswer != value)
                {
                    selectedAnswer = value;
                    OnPropertyChanged("SelectedAnswer");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RatingsViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            answerController = app.answerController;

            Dictionary<Doctor, Answer> doctorReviews = answerController.DoctorRatings();

            Answers = new ObservableCollection<FriendlyAnswer>();
            foreach (Doctor d in doctorReviews.Keys)
                Answers.Add(new FriendlyAnswer(d, doctorReviews[d]));

            Search = "Enter Query";
        }
        public void OnRemove()
        {
            return;
        }

        public bool CanRemove()
        {
            return false;
        }

        public void OnQuery()
        {
            return;
        }

        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    mainWindow.CurrentView = new LoginView();
                    break;
                case "rooms":
                    mainWindow.CurrentView = new RoomTableView();
                    break;
                case "medicine":
                    mainWindow.CurrentView = new MedicineTableView();
                    break;
                case "transfers":
                    mainWindow.CurrentView = new EquipmentTransferTableView();
                    break;
                case "renovations":
                    mainWindow.CurrentView = new RenovationTableView();
                    break;
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
            }
        }

    }

    public class FriendlyAnswer
    {
        public String FullName;
        public double Average;

        public FriendlyAnswer(Doctor doctor, Answer answer)
        {
            FullName = doctor.NameSurname;
            Average = AnswerController.AverageRating(answer);
        }
    }
}

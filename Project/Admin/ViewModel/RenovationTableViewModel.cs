using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class RenovationTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private RenovationController renovationController;

        private String search;
        private ObservableCollection<FriendlyRenovation> renovations;
        private FriendlyRenovation selectedRenovation;

        public String Search
        {
            get { return search; }
            set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        public ObservableCollection<FriendlyRenovation> Renovations
        {
            get { return renovations; }
            set
            {
                if(renovations != value)
                {
                    renovations = value;
                    OnPropertyChanged("Renovations");
                }
            }
        }
        public FriendlyRenovation SelectedRenovation
        {
            get { return selectedRenovation; }
            set
            {
                if (selectedRenovation != value)
                {
                    selectedRenovation = value;
                    OnPropertyChanged("SelectedRenovation");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RenovationTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);

            var app = Application.Current as App;
            renovationController = app.renovationController;

            ObservableCollection<Renovation> renovations = renovationController.ReadAll();

            Renovations = new ObservableCollection<FriendlyRenovation>();
            foreach (Renovation renovation in renovations)
                Renovations.Add(new FriendlyRenovation(renovation));

            Search = "Enter Query";
        }

        public void OnRemove()
        {
            renovationController.DeleteRenovation(SelectedRenovation.Id);
            Renovations.Remove(SelectedRenovation);
        }

        public bool CanRemove()
        {
            return SelectedRenovation is not null;
        }

        public void OnQuery()
        {
            Renovations.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Renovation> renovations = renovationController.ReadAll();
                foreach (Renovation renovationItem in renovations)
                    Renovations.Add(new FriendlyRenovation(renovationItem));
                return;
            }
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
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
                case "answers":
                    mainWindow.CurrentView = new RatingsView();
                    break;
            }
        }

    }

    public class FriendlyRenovation
    {
        public String Id { get; set; }
        public int OriginRoom { get; set; }
        public String Type { get; set; }
        public String EndDate { get; set; }

        public FriendlyRenovation(Renovation renovation)
        {
            Id = renovation.Id;
            OriginRoom = renovation.OriginRoom.RoomNb;
            Type = renovation.Type.ToString();
            EndDate = renovation.EndDate.ToString();
        }
    }
}

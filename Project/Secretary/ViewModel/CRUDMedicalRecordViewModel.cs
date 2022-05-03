using Controller;
using Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class CRUDMedicalRecordViewModel : ViewModelBase
    {
        private readonly MedicalRecordController _medicalRecordController;
        private ObservableCollection<MedicalRecordViewModel> _medicalRecords;

        public ObservableCollection<MedicalRecordViewModel> MedicalRecords => _medicalRecords;

        private MedicalRecordViewModel _medicalRecordViewModel;
        public MedicalRecordViewModel MedicalRecordViewModel
        {
            get { return _medicalRecordViewModel; }
            set { _medicalRecordViewModel = value; OnPropertyChanged(nameof(MedicalRecordViewModel)); }
        }

        public ICommand AddMedicalRecordCommand { get; }
        public ICommand RemoveMedicalRecordCommand { get; }
        public ICommand EditMedicalRecordCommand { get; }

        public CRUDMedicalRecordViewModel()
        {
            var app = System.Windows.Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            _medicalRecords = new ObservableCollection<MedicalRecordViewModel>();

            AddMedicalRecordCommand = new GoToAddMedicalRecordCommand(this);
            EditMedicalRecordCommand = new  GoToEditMedicalRecordCommand(this);
            RemoveMedicalRecordCommand = new DeleteMedicalRecordCommand(this, _medicalRecordController);

            ObservableCollection<MedicalRecord> medicalRecordsFromBase = _medicalRecordController.GetAllMedicalRecords();
            foreach(MedicalRecord mr in medicalRecordsFromBase)
            {
                _medicalRecords.Add(new MedicalRecordViewModel(mr));
            }
        }

    }
}

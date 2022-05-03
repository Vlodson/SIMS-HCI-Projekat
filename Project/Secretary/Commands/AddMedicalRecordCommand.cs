using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ViewModel;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Secretary.Commands
{
    public class AddMedicalRecordCommand : CommandBase
    {
        private readonly AddMedicalRecordViewModel _addMedicalRecordViewModel;
        private readonly MedicalRecordController _medicalRecordController;
        private readonly CRUDMedicalRecordViewModel _crudMedicalRecordViewModel;
        private Window _addMedicalRecord;

        public AddMedicalRecordCommand(AddMedicalRecordViewModel addMedicalRecordViewModel, CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordController medicalRecordController, Window addMedicalRecord)
        {
            _addMedicalRecordViewModel = addMedicalRecordViewModel;
            _medicalRecordController = medicalRecordController;
            _crudMedicalRecordViewModel = cRUDMedicalRecordViewModel;
            _addMedicalRecord = addMedicalRecord;

            _addMedicalRecordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            int newMedicalRecordID = _medicalRecordController.generateID();

            foreach(SelectableItemWrapper<Allergens> allergen in _addMedicalRecordViewModel.AllergensListBoxData)
            {
                //ako je selektovan alergen
                if (allergen.IsSelected)
                {
                    _addMedicalRecordViewModel.Allergens.Add(allergen.Item);
                }
            }

            //pravljenje novog kartona
            _medicalRecordController.CreateMedicalRecord(newMedicalRecordID.ToString(), _addMedicalRecordViewModel.UCIN, _addMedicalRecordViewModel.Name, _addMedicalRecordViewModel.Surname, _addMedicalRecordViewModel.PhoneNumber, _addMedicalRecordViewModel.Mail, _addMedicalRecordViewModel.Adress, _addMedicalRecordViewModel.Gender, _addMedicalRecordViewModel.DateOfBirth, _addMedicalRecordViewModel.BloodType, _addMedicalRecordViewModel.Reports, _addMedicalRecordViewModel.Allergens, new ObservableCollection<HospitalMain.Model.Notification>());

            //update tabele
            UpdateMedicalRecords();

            //zatvaranje prozora
            _addMedicalRecord.Close();
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addMedicalRecordViewModel.UCIN) && !string.IsNullOrEmpty(_addMedicalRecordViewModel.Name) && !string.IsNullOrEmpty(_addMedicalRecordViewModel.Surname) && !string.IsNullOrEmpty(_addMedicalRecordViewModel.Mail) && !string.IsNullOrEmpty(_addMedicalRecordViewModel.Adress) && !string.IsNullOrEmpty(_addMedicalRecordViewModel.PhoneNumber) && base.CanExecute(parameter);
        }

        private void UpdateMedicalRecords()
        {
            _crudMedicalRecordViewModel.MedicalRecords.Clear();
            ObservableCollection<MedicalRecord> medicalRecordsFromBase = _medicalRecordController.GetAllMedicalRecords();

            foreach (MedicalRecord medicalRecord in medicalRecordsFromBase)
            {
                _crudMedicalRecordViewModel.MedicalRecords.Add(new MedicalRecordViewModel(medicalRecord));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AddMedicalRecordViewModel.UCIN) || e.PropertyName == nameof(AddMedicalRecordViewModel.Name) || e.PropertyName == nameof(AddMedicalRecordViewModel.Surname) || e.PropertyName == nameof(AddMedicalRecordViewModel.Mail) || e.PropertyName == nameof(AddMedicalRecordViewModel.Adress) || e.PropertyName == nameof(AddMedicalRecordViewModel.Gender) || e.PropertyName == nameof(AddMedicalRecordViewModel.PhoneNumber) || e.PropertyName == nameof(AddMedicalRecordViewModel.BloodType) || e.PropertyName == nameof(AddMedicalRecordViewModel.DateOfBirth))
            {
                OnCanExecutedChanged();
            }
        }
    }
}

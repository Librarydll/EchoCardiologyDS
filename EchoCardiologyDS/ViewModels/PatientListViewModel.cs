using Caliburn.Micro;
using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.PatientAction;
using EchoCardiologyDS.PfdImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace EchoCardiologyDS.ViewModels
{
	public class PatientListViewModel :Screen
	{
		DBConn _db = null;
		IEventAggregator _eventAggregator;
		IWindowManager manager = new WindowManager();
		IPatientRepository repository;
		public PatientListViewModel(DBConn dB,IEventAggregator eventAggregator)
		{
			if (dB == null) return;
			_db = dB;
			repository = new PatientRepository(_db);
			PatientList = new BindableCollection<Patient>(_db.Select<Patient>().OrderByDescending(x=>x.Id));
			tempData = PatientList;
			_eventAggregator = eventAggregator;
		}

		private string _searchString;

		public string SearchString
		{
			get { return _searchString; }
			set { if (string.IsNullOrWhiteSpace(value)&&tempData!=null) ShowAllData(); _searchString = value; NotifyOfPropertyChange(() => SearchString); }
		}

		private Patient _selectedPatient;

		public Patient SelectedPatient
		{
			get { return _selectedPatient; }
			set { _selectedPatient = value; NotifyOfPropertyChange(() => SelectedPatient); }
		}

		BindableCollection<Patient> tempData;
		private BindableCollection<Patient> _patientList;

		public BindableCollection<Patient> PatientList
		{
			get { return _patientList; }
			set { _patientList = value; NotifyOfPropertyChange(() => PatientList); }
		}

		public void ShowAllData()
		{
			PatientList = tempData;
		}

		public void Search()
		{
			if (string.IsNullOrWhiteSpace(_searchString)) { ShowAllData(); return; }
			PatientList = PatientList.Where(x => !string.IsNullOrWhiteSpace(x.FIO)).
				Where(x => x.FIO.Contains(SearchString)).ToBindable();
		}

		public void CreateWord()
		{
			if (SelectedPatient == null) { MessageBox.Show("Сначала выберите строку!"); return; }
			var patientVm = repository.GetPatientMV(SelectedPatient.Id);
			IView view = new WordImplementation.WordCreator(patientVm,_db);
			new Thread(() => { view.CreateOrOpenDocument(); }).Start();
		}

		public void OpenSelectedPatient()
		{
			manager.ShowWindow(new MainViewModel(_eventAggregator));
		}

		public void DeletePatient()
		{
			if (SelectedPatient == null) { MessageBox.Show("Сначала выберите строку!"); return; }
			MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить {SelectedPatient.FIO}", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
			switch (result)
			{

				case MessageBoxResult.Yes:
					repository.DeletePatient(SelectedPatient.Id);
					PatientList.Remove(SelectedPatient);
					break;

				default:
					break;
			}
		}
	}

	public static class ListExtension
	{
		public static BindableCollection<T> ToBindable<T>(this IEnumerable<T> coll)
		{
			return new BindableCollection<T>(coll);
		}
	}
}

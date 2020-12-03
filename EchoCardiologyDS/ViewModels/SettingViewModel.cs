using Caliburn.Micro;
using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.MysqlModel;
using EchoCardiologyDS.Models.PatientAction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EchoCardiologyDS.ViewModels
{
	public class SettingViewModel :Screen
	{
		DBConn db = null; Hospital hospital = null;
		IWindowManager manager = new WindowManager();
		IPatientRepository repository;
		private IObservableCollection<string> _allItems;

		private string _address;

		public string Address
		{
			get { return _address; }
			set { _address = value; NotifyOfPropertyChange(() => Address); }
		}

		private string _addressUz;

		public string AddressUz
		{
			get { return _addressUz; }
			set { _addressUz = value; NotifyOfPropertyChange(() => AddressUz); }
		}


		private string _pHone;

		public string Phone
		{
			get { return _pHone; }
			set { _pHone = value; NotifyOfPropertyChange(() => Phone); }
		}

		public IObservableCollection<string> AllItems
		{
			get { return _allItems; }
			set { _allItems = value;
				NotifyOfPropertyChange(() => AllItems); }
		}
		private string _inputText;

		public string InputText
		{
			get { return _inputText; }
			set { _inputText = value;
				NotifyOfPropertyChange(() => InputText);  }
		}


		private IObservableCollection<ComboBoxItemName> _gridViewComboBoxName;

		public IObservableCollection<ComboBoxItemName> GridViewComboBoxName
		{
			get { return _gridViewComboBoxName; }
			set { _gridViewComboBoxName = value; NotifyOfPropertyChange(() => GridViewComboBoxName); }
		}

		private ComboBoxItemName _selectedGridViewComboBoxName;

		public ComboBoxItemName SelectedGridViewComboBoxName
		{
			get { return _selectedGridViewComboBoxName; }
			set
			{
				_selectedGridViewComboBoxName = value;
				NotifyOfPropertyChange(() => SelectedGridViewComboBoxName);
				if (value == null) return;
					InputText = value.Name;
			}
		}


		private string _selectedComboBox;

		public string SelectedComboBox
		{
			get { return _selectedComboBox; }
			set
			{
				_selectedComboBox = value;
				DisplaySelectedItem(value);
				NotifyOfPropertyChange(() => SelectedComboBox);
			}
		}
	
		private ICollection<ComboBoxItemName> _itemNames;
		public SettingViewModel(DBConn conn)
		{
			if (conn == null) return;
			db = conn;
			var qeury = db.Select<ComboBoxName>();
			AllItems = new BindableCollection<string> (qeury.Select(x=>x.Name));
			_itemNames = db.Select<ComboBoxItemName>();

			repository = new PatientRepository(db);
			hospital = repository.Hospital.FirstOrDefault();
			Phone = hospital.Phone;
			Address = hospital.Address;
		}

		protected override void OnDeactivate(bool close)
		{
			base.OnDeactivate(close);
		}
		private void DisplaySelectedItem(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return;
			InputText = "";
			var ctx = db.Select<ComboBoxName>();
			var id = ctx.FirstOrDefault(x => x.Name == value);
			var query = _itemNames.Where(x => x.ComboBoxNameId == id.Id);
			GridViewComboBoxName = new BindableCollection<ComboBoxItemName>(query);
		}
		public void AddNewRecordButton()
		{
			if (string.IsNullOrWhiteSpace(SelectedComboBox)) return;
			if (string.IsNullOrWhiteSpace(InputText)) return;
			var id = db.Select<ComboBoxName>("Name", SelectedComboBox).FirstOrDefault().Id;
			db.Insert(new ComboBoxItemName
			{
				ComboBoxNameId = id,
				Name = InputText
			});
			Refresh();
		}
		public void DeleteButton()
		{
			if (SelectedGridViewComboBoxName == null) return;
			MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить {SelectedGridViewComboBoxName.Name}","Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
			switch (result)
			{

				case MessageBoxResult.Yes:
					db.Delete<ComboBoxItemName>(SelectedGridViewComboBoxName.Id);
					Refresh();
					break;

				default:
					break;
			}
		}
		public void UpdateButton()
		{
			if (string.IsNullOrWhiteSpace(SelectedComboBox)) return;
			if (string.IsNullOrWhiteSpace(InputText)) return;
			if (SelectedGridViewComboBoxName == null) return;

			manager.ShowDialog(new UpdateViewModel(InputText, SelectedGridViewComboBoxName.Id));
			Refresh();
		}

		public new void  Refresh()
		{
			_itemNames = db.Select<ComboBoxItemName>();
			DisplaySelectedItem(SelectedComboBox);
			NotifyOfPropertyChange(() => SelectedComboBox);
		}
		public void CloseButton()
		{
			TryClose(true);
		}

		public void SaveHospital()
		{
			if (hospital == null) return;
			hospital.Address = Address;
			hospital.Phone = Phone;
			hospital.AddressUz = AddressUz;
			repository.UpdateHospital(hospital);
		}
	}
}

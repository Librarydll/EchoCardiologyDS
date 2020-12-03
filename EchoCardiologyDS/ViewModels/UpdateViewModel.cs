using Caliburn.Micro;
using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.MysqlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.ViewModels
{
	public class UpdateViewModel :Screen
	{
		DBConn repository;
		public string UpdateTB { get; set; }
		public int id { get; set; }
		public UpdateViewModel(string value,int id)
		{
			UpdateTB = value;
			repository = new DBConn();
			this.id = id;
		}


		public void UpdateButton()
		{
			repository.UpdateComboBoxItem(UpdateTB, id);
			TryClose(true);
		}
	}
}

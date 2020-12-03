using LinqToDB.Data;
using LinqToDB;
using LinqToDB.DataProvider.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DataModels;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public class MySqlContext : IMySqlRepository
	{
		public string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["test"].ConnectionString;

		public ICollection<ComboBoxName> ComboBoxNames => context.ComboBoxNames.LoadWith(x => x.ComboBoxItemNames).ToList();

		public ICollection<ComboBoxItemName> ComboBoxItemNames => context.ComboBoxItemNames.LoadWith(x=>x.ComboBoxNameId).ToList();

		ApplicationDbContext context = null;
		public MySqlContext()
		{
			
		
			
			DataConnection
				   .AddConfiguration(
				   "test",
				   ConnectionString,
				   new MySqlDataProvider());
			DataConnection.DefaultConfiguration = "test";

			//using (TestDB testDB = new TestDB("test"))
			//{
			//	var s = testDB.Comboboxnames.ToList();
			//}

			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
			context = new ApplicationDbContext();
		}

		public void AddComboBoxItem(string itemName, int id)
		{
			throw new NotImplementedException();
		}

		public ICollection<ComboBoxName> GetAllComboBox()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetColumNameByString(string v)
		{
			var ss = ComboBoxNames;
			var t = ComboBoxItemNames;
			var s = context.ComboBoxNames.Where(x => x.Name == v);
			return context.ComboBoxNames.Where(x => x.Name == v).FirstOrDefault().ComboBoxItemNames.Select(x=>x.Name);
		}

		public ICollection<ComboBoxItemName> GetComboBoxItemNames()
		{
			throw new NotImplementedException();
		}
	}
}

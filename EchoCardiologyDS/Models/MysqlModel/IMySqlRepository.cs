using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public interface IMySqlRepository : ISettingRepository, IDisposable
	{
		IEnumerable<string> GetColumNameByString(string v);
		void AddComboBoxItem(string itemName, string comboBoxName);
		void DeleteComboBoxItem(int id);
		void UpdateComboBoxItem(string itemName, int id);
	}
}

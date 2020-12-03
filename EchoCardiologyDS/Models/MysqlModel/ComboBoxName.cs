using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public class ComboBoxName
	{
		public ComboBoxName()
		{
			ComboBoxItemNames = new List<ComboBoxItemName>();
		}
		public int Id { get; set; }
		public string Name { get; set; }

		public IEnumerable<ComboBoxItemName> ComboBoxItemNames { get; set; }
	}
}

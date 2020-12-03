using EchoCardiologyDS.Models.MysqlModel;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
	public class test :DataConnection
	{
		public test() :base("Test")
		{

		}

		public ITable<ComboBoxName> University => GetTable<ComboBoxName>();	
	}
}

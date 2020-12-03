using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public class MysqlConntext : IMySqlRepository
	{
		private MySqlConnection connection = null;
		MySqlCommand command = null;
		MySqlDataReader reader = null;
		public string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        public MysqlConntext(string connstring)
        {
            connection = new MySqlConnection(connstring);
            connection.Open();
        }
		
		public MysqlConntext()
		{
			connection = new MySqlConnection(ConnectionString);
			connection.Open();
		}
		~MysqlConntext()
		{
			connection.Close();
		}


		public ICollection<ComboBoxName> GetAllComboBox()
		{	
			ICollection<ComboBoxName> result =new List<ComboBoxName>();
			try
			{
				command = new MySqlCommand("SELECT *FROM COMBOBOXNAME", connection);
				reader =  command.ExecuteReader();
				while (reader.Read())
				{
					result.Add(new ComboBoxName
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
					});
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				command.Dispose();
				reader.Close();
			}
			return result;
		}

		public void Dispose()
		{
			if (connection != null || connection.State != System.Data.ConnectionState.Closed)
				connection.Close();
		}

		//public ICollection<ComboBoxItemName> GetComboBoxItemNames()
		//{
			
		//	ICollection<ComboBoxItemName> result = new List<ComboBoxItemName>();
		//	try
		//	{
		//		command = new MySqlCommand("SELECT *FROM ComboBoxItemName", connection);
		//		reader = command.ExecuteReader();
		//		while (reader.Read())
		//		{
		//			result.Add(new ComboBoxItemName
		//			{
		//				Id = reader.GetInt32(0),
		//				Name = reader.GetString(1),
		//				ComboBoxNameId =reader.GetInt32(2)
		//			});
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//	finally
		//	{
		//		command.Dispose();
		//		reader.Close();
		//	}
		//	return result;
		//}

		public IEnumerable<string> GetColumNameByString(string text)
		{
            if (reader != null) {
                reader.Close();
                reader.Dispose();
            }
            if(command!=null)
            command.Dispose();
            command = new MySqlCommand($"SELECT Name FROM ComboBoxItemName WHERE ComboBoxNameId in(SELECT ID FROM COMBOBOXNAME Where Name='{text}')", connection);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				yield return reader.GetString(0);
			}
			reader.Close();
			reader.Dispose();
			command.Dispose();
		}

		public void AddComboBoxItem(string itemName,string comboBoxName)
		{
			if (reader != null)
			{
				reader.Close();
				reader.Dispose();
			}
			if (command != null)
				command.Dispose();
			command = new MySqlCommand($"SELECT *FROM COMBOBOXNAME WHERE Name='{comboBoxName}'",connection);
			reader = command.ExecuteReader();
			int id = -1;
			while (reader.Read())
			{
				id = reader.GetInt32(0);
				if (id == -1) return;					
			}
			reader.Close();
			reader.Dispose();
			command.Dispose();

			command = new MySqlCommand($"INSERT INTO ComboBoxItemName (Name,ComboBoxNameId) VALUES(@Name,@ComboBoxNameId)", connection);

			command.Parameters.AddWithValue("Name", itemName);
			command.Parameters.AddWithValue("ComboBoxNameId", id);
			command.ExecuteNonQuery();
			reader.Close();
			reader.Dispose();
			command.Dispose();

		}


		public void UpdateComboBoxItem(string itemName, int id)
		{
			if (command != null)
				command.Dispose();
			command = new MySqlCommand($"UPDATE ComboBoxItemName SET Name ='{itemName}' WHERE Id='{id}'", connection);
			command.ExecuteNonQuery();
			command.Dispose();

		}

		public void DeleteComboBoxItem(int id)
		{
			if (command != null)
				command.Dispose();
			command = new MySqlCommand($"DELETE FROM ComboBoxItemName WHERE Id = {id}", connection);
			command.ExecuteNonQuery();
			command.Dispose();
		}
	}
}

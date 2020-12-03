using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Diagnostics;
using System.Windows;
using System.Configuration;
using System.Data;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace EchoCardiologyDS.Models
{
	public class DBConn 
	{
        private IDbConnection connection;
        public string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        public DBConn()
        {
            Initialize();

        }
		public DBConn(string path)
		{
			connection = new MySqlConnection(path);
			connection.Open();
			connection.Close();
        }

		//Initialize values
		private void Initialize() => connection = new MySqlConnection(ConnectionString);

		//open connection to database
		private bool OpenConnection()
        {
			if (connection.State == ConnectionState.Open) return true;
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show(ex.Message,"Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Update<T>(T model) where T : class
        {
            string query = @"UPDATE {0} SET {1} WHERE Id = @Id";
            List<string> fields = new List<string>();
            List<string> values = new List<string>();

            foreach (var prp in typeof(T).GetProperties())
            {
                try
                {
                    Type type = prp.GetType();
                    if (prp.Name != "Id" && !prp.GetGetMethod().IsVirtual)
                    {
                        fields.Add(string.Format("{0} = @{0}", prp.Name));
                    }
                }
                catch (NullReferenceException)
                {

                }

            }

            string fs = String.Join(", ", fields);

            query = string.Format(query, typeof(T).Name, fs);

            //open connection
            if (this.OpenConnection() == true)
            {
                connection.Execute(query, model);

                //close connection
                this.CloseConnection();
            }
        }
      
        public int Insert<T>(T model) where T : class
        {
            string query = @"INSERT {0}({1}) VALUES({2})";
            List<string> fields = new List<string>();
            List<string> values = new List<string>();

            foreach (var prp in typeof(T).GetProperties())
            {
                try
                {
                    Type type = prp.GetType();
                    if (!prp.GetGetMethod().IsVirtual)
                    {
                        fields.Add("`" + prp.Name + "`");
                        values.Add("@" + prp.Name);
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            string fs = String.Join(", ", fields);
            string vs = String.Join(", ", values);

            query = string.Format(query, typeof(T).Name, fs, vs);
            int ans = 0;
            //open connection
            if (this.OpenConnection() == true)
            {
                ans = connection.Execute(query, model);
                //close connection
                this.CloseConnection();
            }
            return ans;
        }

        public List<T> Select<T>(string field, string value) where T : class
        {
            string query = "select * from " + typeof(T).Name + " where " + field + " = '" + value+"'";
            if (this.OpenConnection() == true)
            {
                try
                {
                    var models = connection.Query<T>(query, null);
                    return models.AsList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                return null;
            }
        }

        public List<T> Select<T>() where T : class
        {
            string query = "select * from " + typeof(T).Name;
            if (this.OpenConnection() == true)
            {
                try
                {
                    var models = connection.Query<T>(query, null);
                    return models.AsList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                return null;
            }
        }

        public List<T> Select<T>(string query) where T : class
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    var models = connection.Query<T>(query, null);
                    return models.AsList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                return null;
            }
        }

		public T Select<T>(int id)
		{
			string query = $"SELECT *FROM {typeof(T).Name} WHERE PatientId={id}";
			if (this.OpenConnection() == true)
			{
				try
				{
					var models = connection.Query<T>(query, null).FirstOrDefault();
					return models;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return default(T);
				}
				finally
				{
					this.CloseConnection();
				}
			}
			else
			{
				return default(T);
			}
		}

		public void DeleteSingle<T>(int id) where T : class
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    var isSucces = connection.Query("DELETE FROM " + typeof(T).Name + " WHERE Id = " + id);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("DELETE SINGLE {0}: {1}", DateTime.Now.ToLongTimeString(), ex.Message));
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
            }
        }

        //Delete statement
        public void Delete<T>(int id)
        {
            string query = "DELETE FROM {0} WHERE Id = {1}";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(string.Format(query, typeof(T).Name, id), connection as MySqlConnection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
      
 
		public IEnumerable<string> GetColumNameByString(string text)
		{
			string query = $"SELECT Name FROM ComboBoxItemName WHERE ComboBoxNameId in(SELECT ID FROM COMBOBOXNAME Where Name='{text}')";

			if (this.OpenConnection() == true)
			{
				try
				{
					IEnumerable<string> models = connection.Query<string>(query, null);
					return models.AsList();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return null;
				}
				finally
				{
					this.CloseConnection();
				}
			}
			else
			{
				return null;
			}

		}

		public void UpdateComboBoxItem(string itemName, int id)
		{
			string query = $"UPDATE ComboBoxItemName SET Name ='{itemName}' WHERE Id='{id}'";
			if (this.OpenConnection() == true)
			{
				connection.Execute(query);
				this.CloseConnection();
			}

		}


        public T GetLastData<T>(string field)
        {
            string query = $"SELECT {field} FROM table ORDER BY {field} DESC LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    var models = connection.Query<T>(query);
                    return models.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return default(T);
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                return default(T);
            }

        }

        public long InsertAndReturnId<T>(T obj) where T:class
        {
            long id = 0;
            try
            {
                id = connection.Insert<T>(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return id;
        }

		public T GetDataById<T>(int id) where T : class
		{
			return connection.Get<T>(id);
		}
	}
}

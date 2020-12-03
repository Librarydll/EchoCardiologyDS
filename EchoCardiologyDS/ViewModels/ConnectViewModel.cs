using Caliburn.Micro;
using EchoCardiologyDS.Models;
using EchoCardiologyDS.Models.MysqlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EchoCardiologyDS.ViewModels
{
    public class ConnectViewModel:Screen
    {
        public string Server { get; set; } = "127.0.0.1";
		public string Login { get; set; } = "root";
        public string Password { get; set; }
		public string DBName { get; set; } = "test";
		public string Port { get; set; } = "3306";
		private DBConn context;
		private IEventAggregator _eventAggregator;
		public ConnectViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}		

        public void Connect()
        {
            string path = $"Data Source={Server};port={Port};Database={DBName};Uid={Login};Pwd={Password};";
            try
            {
				context = new DBConn(path);
			}
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
                return;
            }

			MessageBox.Show("Успешное подключение");
			_eventAggregator.PublishOnUIThread(context);

			this.TryClose(true);
        }

		
    }
}

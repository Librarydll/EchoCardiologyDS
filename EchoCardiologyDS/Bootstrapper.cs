using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using EchoCardiologyDS.ViewModels;

namespace EchoCardiologyDS
{
    public class Bootstrapper:BootstrapperBase
    {
		private SimpleContainer _container;

		public Bootstrapper()
        {
            Initialize();
        }
		protected override void Configure()
		{
			_container = new SimpleContainer();
			_container.Instance(_container);
			_container.Singleton<IWindowManager, WindowManager>();
			_container.Singleton<IEventAggregator, EventAggregator>();
			_container.PerRequest<MainViewModel>();
		}
		protected override object GetInstance(Type service, string key)
		{
			var instance = _container.GetInstance(service, key);
			if (instance != null)
				return instance;
			throw new InvalidOperationException("Could not locate any instances.");
		}
		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _container.GetAllInstances(service);

		}
		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}
		protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}

using EchoCardiologyDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.PfdImplementation
{
	public interface IView
	{
		void CreateDocument();
		void InsertData(DBConn conn, PatientMV p);
		void CreateOrOpenDocument();
	}
}

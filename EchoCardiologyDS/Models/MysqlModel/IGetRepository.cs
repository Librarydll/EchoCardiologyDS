using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public interface IGetRepository
	{
		IEnumerable<string> GetApparatNames();
		IEnumerable<string> GetQualityNames();
		IEnumerable<string> GetDoctorNames();
		IEnumerable<string> GetEcgNames();
		IEnumerable<string> GetStomachNames();
		IEnumerable<string> GetTypeThicknessMiokardNames();
		IEnumerable<string> GetActionMJPNames();
		IEnumerable<string> GetEchoMiokardNames();
		IEnumerable<string> GetEvaluateIndexNames();
		IEnumerable<string> GetFVNames();
		IEnumerable<string> GetCollNames();
		IEnumerable<string> GetValveNames();
		IEnumerable<string> GetINSNames();

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.MysqlModel
{
	public interface ISettingRepository
	{
		ICollection<ComboBoxName> GetAllComboBox();
	}
}

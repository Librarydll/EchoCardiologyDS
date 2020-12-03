using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Core
{
	public class AgeList
	{
		public string Name { get; set; }
		public int Code { get; set; }
		public AgeList(string d, int c)
		{
			Name = d;
			Code = c;
		}
		public static IEnumerable<AgeList> GetAgeList()
		{
			yield return new AgeList("", 0);
			yield return new AgeList ("1-й месяц",1);
			yield return new AgeList("1-2 г",2);
			yield return new AgeList("3-7 л",3);
			yield return new AgeList("8-13 л",4);
		}
	}
}

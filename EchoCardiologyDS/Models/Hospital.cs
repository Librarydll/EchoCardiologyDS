using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
	[Table(Name ="hospital")]
	public class Hospital
	{
		public int Id { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string AddressUz { get; set; }

	}
}

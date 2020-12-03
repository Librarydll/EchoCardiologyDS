using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{
	[Table(Name ="segment")]
	public class Segment
	{
		public int Id { get; set; }
		public byte[] FirstSegment { get; set; }
		public byte[] SecondSegment { get; set; }
		public byte[] ThirdSegment { get; set; }
		public byte[] ForthSegment { get; set; }
		public byte[] FifthSegment { get; set; }
		public byte[] SixSegment { get; set; }
		public byte[] CirlceSegment { get; set; }
		public byte[] MiddleCircleSegment { get; set; }
		public int PatientId { get; set; }
	}
}

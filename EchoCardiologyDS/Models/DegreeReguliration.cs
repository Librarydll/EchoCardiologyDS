using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "degreereguliration")]
    public class DegreeReguliration
	{
        public int Id { get; set; }

        public string MK { get; set; }
		public string TK { get; set; }
		public string AK { get; set; }
		public string LK { get; set; }
        public int PatientId { get; set; }
    }
}

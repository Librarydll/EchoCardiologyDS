using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "aorticvalve")]
    public class AorticValve
	{
        public int Id { get; set; }
        public string Comment { get; set; }
		public string Method { get; set; }
		public string Flow { get; set; }
		public string PHT { get; set; }
		public string VenaContrcata { get; set; }
        public int PatientId { get; set; }
    }
}

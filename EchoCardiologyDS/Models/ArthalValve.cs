using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "arthalvalve")]
    public class ArthalValve
    {
        public int Id { get; set; }

        public string Comment { get; set; }
		public string Flow { get; set; }
		public string PTH { get; set; }
		public string VenaContracta { get; set; }
         public int PatientId { get; set; }

	}
}

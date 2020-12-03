using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "mkmr")]

    public class MKMR
	{
        public int Id { get; set; }

        public string SideSizeFK { get; set; }
		public string IntercommissioningSizeFK { get; set; }
		public string LengthDiastol { get; set; }
		public string LengthSistol { get; set; }
		public string HeadZMPM { get; set; }
		public string Depth { get; set; }
		public string Prot { get; set; }
		public string CSept { get; set; }
		public string STening { get; set; }
		public string Rvol { get; set; }
		public string RF { get; set; }
		public string FLGap { get; set; }
        public int PatientId { get; set; }

    }
}

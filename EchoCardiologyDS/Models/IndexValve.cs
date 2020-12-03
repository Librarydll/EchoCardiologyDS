using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    //PPT
    [Table(Name = "indexvalve")]
	public class IndexValve
	{
        public int Id { get; set; }

        public string KDR { get; set; }
		public string LPP { get; set; }
		public string AO { get; set; }
		public string WeigthLJ { get; set; }
		public string KDO { get; set; }
		public string KSO { get; set; }
		public string VolumeLP { get; set; }
		public string VolumePP { get; set; }
		public string STK { get; set; }
		public string NA { get; set; }
        public int PatientId { get; set; }
    }
}

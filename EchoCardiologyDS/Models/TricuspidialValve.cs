using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "tricuspidialvalve")]

    public class TricuspidialValve
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string PISA { get; set; }
        public string VenaContracta { get; set; }
		public string RVOL { get; set; }
		public string FK { get; set; }
        public string EROA { get; set; }
        public int PatientId { get; set; }
    }
}

using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "pulmonaryvalve")]

    public class PulmonaryValve
	{
        public int Id { get; set; }
        public string SelectedText { get; set; }
		public string Comment { get; set; }
        public int PatientId { get; set; }
    }
}

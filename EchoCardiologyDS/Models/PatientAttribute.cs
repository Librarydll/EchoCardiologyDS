using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "patientattribute")]
    public class PatientAttribute
    {
        public int Id { get; set; }     
        public string LeftAtrium { get; set; }
        public string SelectedRightStomach { get; set; }
        public string RightStomachComment { get; set; }
        public int PatientId { get; set; }
    }
}

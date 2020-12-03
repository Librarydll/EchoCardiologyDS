using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "ecg")]

    public class ECG
    {
        public int Id { get; set; }
        public string Rhythm { get; set; }
        public string FirstTextBox { get; set; }
        public string SecondTextBox { get; set; }
        public int PatientId { get; set; }
    }
}

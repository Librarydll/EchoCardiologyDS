using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name ="aorta")]
   public class Aorta
    {
        public int Id { get; set; }
        public string Circle { get; set; }
        public string Sinus { get; set; }
        public string Contact { get; set; }
        public string Arise { get; set; }
        public string Arc { get; set; }
        public string Departament { get; set; }
        public string OpenAorthValve { get; set; }
        public int PatientId { get; set; }
    }
}

using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "leftstomachfunction")]
    public class LeftStomachFunction
    {
        public int Id { get; set; }

        public string PickE { get; set; }
        public string PickA { get; set; }
        public string EA { get; set; }
        public string ES { get; set; }
        public string El { get; set; }
        public string EE { get; set; }
        public string DT { get; set; }
        public string IVRS { get; set; }
        public string S { get; set; }
        public string D { get; set; }
        public string SD { get; set; }
        public string Z { get; set; }

        public string MaxGradientMitralValve { get; set; }
        public string AverageGradientMitralValve { get; set; }
        public int PatientId { get; set; }
    }
}

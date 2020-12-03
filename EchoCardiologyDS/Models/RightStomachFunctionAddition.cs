using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "rightstomachfunctionaddition")]

    public class RightStomachFunctionAddition
    {
        public int Id { get; set; }
        public string MaxValomeLeftAtrium { get; set; }
        public string MaxValomeRightAtrium { get; set; }
        public string LengthLJ { get; set; }
        public string WidthLJ { get; set; }
        public string LengthLP { get; set; }
        public string WidthLP { get; set; }
        public string LengthPJ { get; set; }
        public string WidthPJ { get; set; }
        public string LengthPP { get; set; }
        public string WidthPP { get; set; }
        public string SquareRigthAtrium { get; set; }
        public int PatientId { get; set; }
    }
}

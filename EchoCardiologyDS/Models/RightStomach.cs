using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "rightstomach")]
    public class RightStomach
    {
        public int Id { get; set; }

        public string BazalPj { get; set; }
        public string ProksiVTPJ { get; set; }
        public string S { get; set; }
        public string MaxdPJ { get; set; }
        public string DistalVTPJ { get; set; }
        public string Fac { get; set; }
        public string ActionFKTK { get; set; }
        public string WidthLa { get; set; }
        public string GLS { get; set; }
        public string Width { get; set; }
        public string RigthLA { get; set; }
        public string Dpdt { get; set; }
		public string SelectedColl { get; set; }
		public string MaxGradientTricuspidil { get; set; }
        public string AverageDLA { get; set; }
        public string PressurePP { get; set; }
        public string DDLA { get; set; }
        public string SistolPressureLA { get; set; }
        public string DZLK { get; set; }
        public int PatientId { get; set; }

    }
}

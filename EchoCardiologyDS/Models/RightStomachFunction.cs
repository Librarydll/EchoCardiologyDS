using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "rightstomachfunction")]

    public class RightStomachFunction	
    {
        public int Id { get; set; }
        public string PickE { get; set; }
        public string PickA { get; set; }
        public string EA { get; set; }
        public string DT { get; set; }
        public string EL { get; set; }
        public string EEL { get; set; }
        public string MaxGradientTricuspidil { get; set; }
        public string AverageGradientPressureTrucuspidil { get; set; }
        public string VelocityLeftStomach { get; set; }
        public string MaxGradientLeftStomach { get; set; }
        public string VelocityAortValve { get; set; }
        public string MaxGradientAortValue { get; set; }
		public string AverageGradientAortValue { get; set; }
		public string LastVolumeLJ { get; set; }
		public string FV { get; set; }
		public string SelectedFv { get; set; }
		public string KSO { get; set; }
		public string UOK { get; set; }
		public string MOK { get; set; }
		public string GLS { get; set; }
		public string DPDT { get; set; }
		
        public int PatientId { get; set; }
    }
}

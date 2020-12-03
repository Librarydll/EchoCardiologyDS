using System;
using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "patienvalue")]

    public class PatienValue
    {
        public int Id { get; set; }
        public string LastDiastolSizeLeftStomach { get; set; }
        public string ThicknessSegment { get; set; }
        public string SelectedTypeThicknessMiocard { get; set; }
        public string TypeComment { get; set; }	
        public string ExoMiocard { get; set; }
        public string ThicknessMJPSislotu { get; set; }
        public string AmplitudeMovementMJP { get; set; }
        public string ThicknessLowerWallLeftStomach { get; set; }
        public string ThicknessLowerWallLeftStomachSislotu { get; set; }
        public string AmplitudeMovementLowerWall { get; set; }
        public string LastSislotSizeLeftStomach { get; set; }
        public string FractionAcceleration { get; set; }
        public string VelocityVavlvePulmonaryArtery { get; set; }
        public string MaxGradientPressurePulmonaryArtery { get; set; }
        public string SovleIndexWeight { get; set; }
        public string MovementMJP { get; set; }
        public string INS { get; set; }
        public string SelectedINS { get; set; }
        public string ThicknessMejPereMJP { get; set; }
		public string RelativeThicknessLeftStomach { get; set; }
		public string LeftAtrium { get; set; }
		public int PatientId { get; set; }

  
    }
}

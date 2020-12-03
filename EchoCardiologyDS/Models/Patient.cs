using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name ="patient")]
    public class Patient
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Jender { get; set; }
        public double Height { get; set; }
        public double Weigth { get; set; }
        public string DirectDepartment { get; set; }
        public string ResearchName { get; set; }
        public string CardNumber { get; set; }
        public DateTime? ResearchDateTime { get; set; }
        public string DoctorName { get; set; }
        public string ResearchAim { get; set; }
        public string Conclusion { get; set; }
        public string BirthDay { get; set; }
		public string SelectedApparat { get; set; }
		public string SelectedQualityVizuality { get; set; }
		public string Recomendation { get; set; }
		public string Commentary { get; set; }
		public string CommentarySegment { get; set; }
		public int ChildAgeCode { get; set; }
		public int PatientAttributeId { get; set; }

		public int PatienValueId { get; set; }

		public int ECGId { get; set; }

		public int AortaId { get; set; }

		public int LeftStomachId { get; set; }

		public int RightStomachFunctionId { get; set; }

		public int RightStomachFunctionAdditionId { get; set; }

		public int RightStomachId { get; set; }

		public int DegreeRegulirationId { get; set; }

		public int MitralValveId { get; set; }

		public int PulmonaryValveId { get; set; }

		public int MKMRId { get; set; }

		public int AorticValveId { get; set; }

		public int TricuspidialValveId { get; set; }
		public int IndexId { get; set; }
		public int SegmentId { get; set; }
	}

}

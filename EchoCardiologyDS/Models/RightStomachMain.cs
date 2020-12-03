using System.Data.Linq.Mapping;

namespace EchoCardiologyDS.Models
{
    [Table(Name = "rightstomachmain")]
    public class RightStomachMain
    {
        public int Id { get; set; }     
        public string SelectedRightStomach { get; set; }
        public string RightStomachComment { get; set; }
		public string ThicknessWallRightStomach { get; set; }

		public int PatientId { get; set; }
    }
}

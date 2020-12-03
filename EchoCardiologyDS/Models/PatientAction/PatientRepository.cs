using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.PatientAction
{
    public class PatientRepository : IPatientRepository
    {
        private DBConn conn;
        public PatientRepository(DBConn bConn)
        {
            conn = bConn;
        }
        public Dictionary<string, long> IdRepository { get; set; } = new Dictionary<string, long>();
		public IEnumerable<Hospital> Hospital { get => conn.Select<Hospital>(); }

		public long InsertAndReturnId<T>(T obj) where T : class
        {
           return  conn.InsertAndReturnId(obj);
        }

        public void UpdatePatient(Patient patient)
        {
            if (!IdRepository.ContainsKey("Patient")) return;
            patient.Id = (int)IdRepository["Patient"];
            patient.AortaId = IdRepository.ContainsKey("Aorta") ? (int)IdRepository["Aorta"] : 0;
            patient.AorticValveId = IdRepository.ContainsKey("AorticValve") ? (int)IdRepository["AorticValve"] : 0;
            patient.DegreeRegulirationId = IdRepository.ContainsKey("DegreeReguliration") ? (int)IdRepository["DegreeReguliration"] : 0;
            patient.ECGId = IdRepository.ContainsKey("ECG") ? (int)IdRepository["ECG"] : 0;
            patient.IndexId = IdRepository.ContainsKey("Index") ? (int)IdRepository["Index"] : 0;
            patient.LeftStomachId = IdRepository.ContainsKey("LeftStomach") ? (int)IdRepository["LeftStomach"] : 0;
            patient.MitralValveId = IdRepository.ContainsKey("MitralValve") ? (int)IdRepository["MitralValve"] : 0;
            patient.MKMRId = IdRepository.ContainsKey("MKMR") ? (int)IdRepository["MKMR"] : 0;
            patient.PatientAttributeId = IdRepository.ContainsKey("PatientAttribute") ? (int)IdRepository["PatientAttribute"] : 0;
            patient.PatienValueId = IdRepository.ContainsKey("PatienValue") ? (int)IdRepository["PatienValue"] : 0;
            patient.PulmonaryValveId = IdRepository.ContainsKey("PulmonaryValve") ? (int)IdRepository["PulmonaryValve"] : 0;
            patient.RightStomachId = IdRepository.ContainsKey("RightStomach") ? (int)IdRepository["RightStomach"] : 0;
            patient.RightStomachFunctionId = IdRepository.ContainsKey("RightStomachFunction") ? (int)IdRepository["RightStomachFunction"] : 0;
            patient.RightStomachFunctionAdditionId = IdRepository.ContainsKey("RightStomachFunctionAddition") ? (int)IdRepository["RightStomachFunctionAddition"] : 0;
            patient.TricuspidialValveId = IdRepository.ContainsKey("TricuspidialValve") ? (int)IdRepository["TricuspidialValve"] : 0;
			patient.SegmentId = IdRepository.ContainsKey("SegmentId") ? (int)IdRepository["SegmentId"] : 0;

			conn.Update(patient);
        }

        public void Initialize(PatientMV patient)
        {
            IdRepository["Patient"] = this.InsertAndReturnId(patient.Patient);
			int patientId =(int)IdRepository["Patient"];
			patient.Aorta.PatientId = patientId;
			patient.AorticValve.PatientId = patientId;
			patient.DegreeReguliration.PatientId = patientId;
			patient.ECG.PatientId = patientId;
			patient.LeftStomachFunction.PatientId = patientId;
			patient.MitralValve.PatientId = patientId;
			patient.MKMR.PatientId = patientId;
			patient.RightStomachMain.PatientId = patientId;
			patient.PatienValue.PatientId = patientId;
			patient.PulmonaryValve.PatientId = patientId;
			patient.RightStomach.PatientId = patientId;
			patient.RightStomachFunction.PatientId = patientId;
			patient.RightStomachFunctionAddition.PatientId = patientId;
			patient.TricuspidialValve.PatientId = patientId;
			patient.Segment.PatientId = patientId;
			IdRepository["Aorta"] = this.InsertAndReturnId(patient.Aorta);
            IdRepository["AorticValve"] = this.InsertAndReturnId(patient.AorticValve);
            IdRepository["DegreeReguliration"] = this.InsertAndReturnId(patient.DegreeReguliration);
            IdRepository["ECG"] = this.InsertAndReturnId(patient.ECG);
            IdRepository["LeftStomach"] = this.InsertAndReturnId(patient.LeftStomachFunction);
            IdRepository["MitralValve"] = this.InsertAndReturnId(patient.MitralValve);
            IdRepository["MKMR"] = this.InsertAndReturnId(patient.MKMR);
            IdRepository["PatientAttribute"] = this.InsertAndReturnId(patient.RightStomachMain);
            IdRepository["PatienValue"] = this.InsertAndReturnId(patient.PatienValue);
            IdRepository["PulmonaryValve"] = this.InsertAndReturnId(patient.PulmonaryValve);
            IdRepository["RightStomach"] = this.InsertAndReturnId(patient.RightStomach);
            IdRepository["RightStomachFunction"] = this.InsertAndReturnId(patient.RightStomachFunction);
            IdRepository["RightStomachFunctionAddition"] = this.InsertAndReturnId(patient.RightStomachFunctionAddition);
            IdRepository["TricuspidialValve"] = this.InsertAndReturnId(patient.TricuspidialValve);
			IdRepository["Segment"] = this.InsertAndReturnId(patient.Segment);

			this.UpdatePatient(patient.Patient);
        }

		public PatientMV GetPatientMV(int patientId)
		{
			return new PatientMV
			{
				Patient = GetPatientT<Patient>(patientId),
				Aorta = SelectPatientT<Aorta>(patientId),
				AorticValve = SelectPatientT<AorticValve>(patientId),
				DegreeReguliration = SelectPatientT<DegreeReguliration>(patientId),
				ECG = SelectPatientT<ECG>(patientId),
				LeftStomachFunction = SelectPatientT<LeftStomachFunction>(patientId),
				MitralValve = SelectPatientT<MitralValve>(patientId),
				MKMR = SelectPatientT<MKMR>(patientId),
				RightStomachMain = SelectPatientT<RightStomachMain>(patientId),
				PatienValue = SelectPatientT<PatienValue>(patientId),
				PulmonaryValve = SelectPatientT<PulmonaryValve>(patientId),
				RightStomach = SelectPatientT<RightStomach>(patientId),
				RightStomachFunction = SelectPatientT<RightStomachFunction>(patientId),
				RightStomachFunctionAddition = SelectPatientT<RightStomachFunctionAddition>(patientId),
				TricuspidialValve = SelectPatientT<TricuspidialValve>(patientId),
				Segment = SelectPatientT<Segment>(patientId)
			};
		}

		public T GetPatientT<T>(int id) where T : class
		{
			return	conn.GetDataById<T>(id);
		}

		public T SelectPatientT<T>(int id) where T : class
		{
			return conn.Select<T>(id);
		}

		public void DeletePatient(int id)
		{
			conn.Delete<Patient>(id);
			conn.Delete<Aorta>(id);
			conn.Delete<AorticValve>(id);
			conn.Delete<DegreeReguliration>(id);
			conn.Delete<ECG>(id);
			conn.Delete<LeftStomachFunction>(id);
			conn.Delete<MitralValve>(id);
			conn.Delete<MKMR>(id);
			conn.Delete<PatienValue>(id);
			conn.Delete<PulmonaryValve>(id);
			conn.Delete<RightStomach>(id);
			conn.Delete<RightStomachFunction>(id);
			conn.Delete<RightStomachFunctionAddition>(id);
			conn.Delete<RightStomachMain>(id);
			conn.Delete<Segment>(id);
			conn.Delete<TricuspidialValve>(id);

		}

		public void UpdateHospital(Hospital hospital)
		{
			if (hospital == null) return;

			conn.Update<Hospital>(hospital);
		}
	}
}

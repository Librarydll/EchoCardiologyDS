using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models.PatientAction
{
    public interface IPatientRepository
    {
        long InsertAndReturnId<T>(T obj) where T : class;
        Dictionary<string, long> IdRepository { get; set; }
        void UpdatePatient(Patient patient);
        void Initialize(PatientMV patient);
		PatientMV GetPatientMV(int patientId);
		T GetPatientT<T>(int id) where T : class;
		T SelectPatientT<T>(int id) where T:class;
		void DeletePatient(int id);
		IEnumerable<Hospital> Hospital { get; }
		void UpdateHospital(Hospital hospital);
	}
}

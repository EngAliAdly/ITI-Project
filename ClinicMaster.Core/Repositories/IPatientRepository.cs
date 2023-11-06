using ClinicMaster.Core.Models;

namespace ClinicMaster.Core.Repositories
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetPatients();
        IEnumerable<Patient> GetRecentPatients();
        Patient GetPatient(int id);
        void Add(Patient patient);
        void Remove(Patient patient);
    }
}

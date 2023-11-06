using ClinicMaster.Core.Repositories;

namespace ClinicMaster.Core
{
    public interface IUnitOfWork
    {
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        IAttendanceRepository Attandences { get; }
        ICityRepository Cities { get; }
        IDoctorRepository Doctors { get; }
        ISpecializationRepository Specializations { get; }
        IApplicationUserRepository Users { get; }

        void Complete();
    }
}

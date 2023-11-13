using ClinicMaster.Core;
using ClinicMaster.Core.Repositories;
using ClinicMaster.Infrastructure.Data;
using ClinicMaster.Infrastructure.Repositories;

namespace ClinicMaster.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IPatientRepository Patients { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IAttendanceRepository Attandences { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public IAssistantRepository Assistants { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }
        public IApplicationUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Patients = new PatientRepository(context);
            Appointments = new AppointmentRepository(context);
            Attandences = new AttendanceRepository(context);
            Cities = new CityRepository(context);
            Doctors = new DoctorRepository(context);
            Assistants = new AssistantRepository(context);
            Specializations = new SpecializationRepository(context);
            Users = new ApplicationUserRepository(context);

        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}

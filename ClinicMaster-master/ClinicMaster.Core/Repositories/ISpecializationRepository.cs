using ClinicMaster.Core.Models;

namespace ClinicMaster.Core.Repositories
{
    public interface ISpecializationRepository
    {
        IEnumerable<Specialization> GetSpecializations();
    }
}

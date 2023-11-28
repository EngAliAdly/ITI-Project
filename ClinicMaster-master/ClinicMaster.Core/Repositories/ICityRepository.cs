using ClinicMaster.Core.Models;

namespace ClinicMaster.Core.Repositories
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();
    }
}
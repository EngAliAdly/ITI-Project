using ClinicMaster.Core.Models;
using ClinicMaster.Core.Repositories;
using ClinicMaster.Infrastructure.Data;

namespace ClinicMaster.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }
    }
}

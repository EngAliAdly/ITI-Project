using ClinicMaster.Core.Models;
using ClinicMaster.Core.Repositories;
using ClinicMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicMaster.Infrastructure.Repositories
{
    public class AssistantRepository : IAssistantRepository
    {
        private readonly ApplicationDbContext _context;
        public AssistantRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Assistant assistant)
        {
            _context.Assistants.Add(assistant);
        }

        public Assistant GetAssistant(int id)
        {
            return _context.Assistants
                .Include(u => u.Physician)
                .SingleOrDefault(d => d.Id == id);
        }

        public IEnumerable<Assistant> GetAssistants()
        {
            return _context.Assistants
                .ToList();
        }

        public Assistant GetProfile(string userId)
        {
            return _context.Assistants
                .Include(u => u.Physician)
                .SingleOrDefault(d => d.PhysicianId == userId);
        }
    }
}

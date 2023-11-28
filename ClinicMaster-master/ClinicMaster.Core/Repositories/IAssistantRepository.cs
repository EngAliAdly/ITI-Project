using ClinicMaster.Core.Models;

namespace ClinicMaster.Core.Repositories
{
    public interface IAssistantRepository
    {
        IEnumerable<Assistant> GetAssistants();
        Assistant GetAssistant(int id);
        Assistant GetProfile(string userId);
        void Add(Assistant assistant);
    }
}

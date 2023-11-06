using ClinicMaster.Core.Models.Extend;
using ClinicMaster.Core.ViewModel;

namespace ClinicMaster.Core.Repositories
{
    public interface IApplicationUserRepository
    {
        List<UserViewModel> GetUsers();
        ApplicationUser GetUser(string id);
    }
}

using ClinicMaster.Core.Models.Extend;
using ClinicMaster.Core.Repositories;
using ClinicMaster.Core.ViewModel;
using ClinicMaster.Infrastructure.Data;

namespace ClinicMaster.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Get
        //public List<UserViewModel> GetUsers()
        //{
        //    return (from user in _context.Users
        //            from userRole in user.Roles
        //            join role in _context.Roles
        //                on userRole.RoleId equals role.Id
        //            select new UserViewModel()
        //            {
        //                Id = user.Id,
        //                Email = user.Email,
        //                Role = role.Name,
        //                IsActive = user.IsActive
        //            }).ToList();
        //} 
        #endregion

        public List<UserViewModel> GetUsers()
        {
            var usersWithRoles = (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.Id
                                  select new
                                  {
                                      UserId = user.Id,
                                      Email = user.Email,
                                      RoleName = role.Name,
                                      IsActive = user.IsActive
                                  }).ToList();

            var userViewModels = usersWithRoles.Select(u => new UserViewModel
            {
                Id = u.UserId,
                Email = u.Email,
                Role = u.RoleName,
                IsActive = u.IsActive
            }).ToList();

            return userViewModels;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }
    }
}

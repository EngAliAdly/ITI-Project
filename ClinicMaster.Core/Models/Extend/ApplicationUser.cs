using Microsoft.AspNetCore.Identity;
namespace ClinicMaster.Core.Models.Extend
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

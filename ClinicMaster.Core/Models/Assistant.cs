using ClinicMaster.Core.Models.Extend;

namespace ClinicMaster.Core.Models
{
    public class Assistant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhysicianId { get; set; }
        public ApplicationUser Physician { get; set; }
    }
}

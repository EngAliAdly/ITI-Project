using System.ComponentModel.DataAnnotations;

namespace ClinicMaster.Core.ViewModel
{
    public class AssistantFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 Numbers")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        public RegisterEmpViewModel? RegisterEmpViewModel { get; set; }
    }
}

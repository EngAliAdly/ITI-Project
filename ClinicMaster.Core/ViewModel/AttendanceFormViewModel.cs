using System.ComponentModel.DataAnnotations;

namespace ClinicMaster.Core.ViewModel
{
    public class AttendanceFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ClinicRemarks { get; set; }

        [Required]
        [StringLength(255)]
        public string? Diagnosis { get; set; }

        public string SecondDiagnosis { get; set; }
        public string ThirdDiagnosis { get; set; }

        [Required]
        public string Therapy { get; set; }


        public DateTime Date { get; set; }

        public string? Heading { get; set; }

        [Required]
        public int Patient { get; set; }


        [Required]
        public int Doctor { get; set; }
    }
}

using ClinicMaster.Core.Attribute;
using ClinicMaster.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ClinicMaster.Core.ViewModel
{
    public class AppointmentFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [ValidDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }


        [Required]
        public string Detail { get; set; }

        [Required]
        public bool Status { get; set; }


        [Required]
        public int Patient { get; set; }
        public IEnumerable<Patient>? Patients { get; set; }

        [Required]
        public int Doctor { get; set; }

        public IEnumerable<Doctor>? Doctors { get; set; }
        public string? Heading { get; set; }

        public IEnumerable<Appointment>? Appointments { get; set; }

        public DateTime GetStartDateTime()
        {
            string dateTimeString = $"{Date} {Time}";
            DateTime result;

            if (DateTime.TryParseExact(dateTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                // Handle parsing failure, perhaps throw an exception or return a default value
                throw new FormatException($"The date and time '{dateTimeString}' is not in the expected format.");
            }
        }
    }
}

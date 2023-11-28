using ClinicMaster.Core.Attribute;
using ClinicMaster.Core.Helpers;
using ClinicMaster.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ClinicMaster.Core.ViewModel
{
    public class PatientFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Gender Sex { get; set; }
        [Required]
        [ValidDate]
        public string BirthDate { get; set; }


        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 Numbers")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Height must be between 2 and 3 Numbers")]
        public string? Height { get; set; }
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Weight must be between 2 and 3 Numbers")]
        public string? Weight { get; set; }

        public byte City { get; set; }

        public DateTime Date { get; set; }

        public string? Heading { get; set; }

        public DateTime GetBirthDate()
        {
            // TODO: Validate BirthDate 

            string dateFormat = "dd/MM/yyyy";

            // Use DateTime.TryParseExact to handle parsing errors gracefully
            if (DateTime.TryParseExact(BirthDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                // Handle parsing error
                throw new ArgumentException("Invalid date format");
            }
        }

        public IEnumerable<City>? Cities { get; set; }



        //public string Action
        //{
        //    get
        //    {
        //        Expression<Func<PatientsController, ActionResult>> update = (c => c.Update(this));
        //        Expression<Func<PatientsController, ActionResult>> create = (c => c.Create(this));

        //        var action = (Id != 0) ? update : create;
        //        return (action.Body as MethodCallExpression).Method.Name;

        //    }
        //}

        #region for dropdownlist

        public IEnumerable<SelectListItem> GendersList
        {
            get { return ClinicMgtHelpers.GenderToSelectList(); }
            set { }
        }

        #endregion
    }
}

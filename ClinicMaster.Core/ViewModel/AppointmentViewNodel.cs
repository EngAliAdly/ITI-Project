using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMaster.Core.ViewModel
{
    public class AppointmentViewNodel
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime StartDateTime { get; set; }
        public string DoctorName { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
    }
}

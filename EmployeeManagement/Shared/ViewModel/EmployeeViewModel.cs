using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Shared.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "* Name is required")]
        [MinLength(3)]
        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "* Email Id is required")]
        [EmailAddress]
        public string? EmailId { get; set; }

        [Required(ErrorMessage = "* Date Of Joining is required")]
        public DateTime? Doj { get; set; }

        [Required(ErrorMessage = "* Department Id is required")]
        public int DepartmentId { get; set; }
    }
}

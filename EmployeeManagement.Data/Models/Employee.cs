using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Data.Models;

public partial class Employee
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

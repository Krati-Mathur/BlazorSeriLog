using EmployeeManagement.Server.Repository;
using EmployeeManagement.Shared.ViewModel;
using EmployeeManagement.Server.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;

namespace EmployeeManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;
        //private readonly IDepartmentManager _departmentManager;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeManager employeeManager,ILogger<EmployeeController> logger)
        // , IDepartmentManager departmentManager)
        {
            _employeeManager = employeeManager ??
                throw new ArgumentNullException(nameof(employeeManager));
            //_departmentManager = departmentManager ??
            //    throw new ArgumentNullException(nameof(departmentManager));
            _logger = logger;
        }

        [HttpGet]
        [Route("GetEmployees/{page}/{pageSize}/{sortColumn}/{sortDirection}")]
        public async Task<ActionResult> Get(int page, int pageSize, string sortColumn, string sortDirection)
        {
            _logger.LogInformation("serilog Employees() called");
            var employee = await _employeeManager.GetEmployees(page, pageSize, sortColumn, sortDirection);
            return Ok(employee);
        }

        [HttpGet]
        [Route("GetEmployeeByID/{ID}")]
        public async Task<IActionResult> GetEmpByID(int ID)
        {
            var result = await _employeeManager.GetEmployeeByID(ID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> Post(EmployeeViewModel emp)
        {
            if (emp == null)
            {
                return BadRequest();
            }
            var result = await _employeeManager.CreateEmployee(emp);
            if (result.EmployeeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
            return Ok("Created Successfully");
        }

        [HttpPut]
        [Route("EditEmployee")]
        public async Task<IActionResult> Put(EmployeeViewModel emp)
        {
            await _employeeManager.EditEmployee(emp);
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        [Route("DeleteEmployee/{ID}")]
        public async Task <IActionResult> Delete(int ID) 
        {
            var result = await _employeeManager.DeleteEmployee(ID);
            return Ok(result);
        }

	}
}

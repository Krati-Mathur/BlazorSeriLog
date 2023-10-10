using EmployeeManagement.Shared.ViewModel;
using EmployeeManagement.Server.Repository;

namespace EmployeeManagement.Server.Manager
{
	public class EmployeeManager : IEmployeeManager
	{

		private readonly IEmployeeRepository _employeeRepository;
		public EmployeeManager(IEmployeeRepository employeeRepository) 
		{ 
			_employeeRepository = employeeRepository;
		}

		public async Task<GridParameterViewModel> GetEmployees(int page, int pageSize, string sortColumn, string sortDirection)
		{
			try
			{
				var result = await _employeeRepository.GetEmployees(page, pageSize, sortColumn, sortDirection);
				return result;
			}
			catch (Exception ex)
            {
                return new GridParameterViewModel();
            }

        }

        public async Task<EmployeeViewModel> GetEmployeeByID(int ID)
        {
            return await _employeeRepository.GetEmployeeByID(ID);
        }
        public async Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel objEmployee)
		{
			return await _employeeRepository.CreateEmployee(objEmployee);
		}

		public async Task<EmployeeViewModel> EditEmployee(EmployeeViewModel objEmployee)
		{
			return await _employeeRepository.EditEmployee(objEmployee);
		}

		public async Task<bool> DeleteEmployee(int ID)
		{
			return await _employeeRepository.DeleteEmployee(ID);
		}
	}
}

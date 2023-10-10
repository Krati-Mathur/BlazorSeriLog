using EmployeeManagement.Shared.ViewModel;
namespace EmployeeManagement.Server.Repository
{
    public interface IEmployeeManager
    {
        Task<GridParameterViewModel> GetEmployees(int page, int pageSize, string sortColumn, string sortDirection);
        Task<EmployeeViewModel> GetEmployeeByID(int ID);
        Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel objEmployee);
        Task<EmployeeViewModel> EditEmployee(EmployeeViewModel objEmployee);
        Task<bool> DeleteEmployee(int ID);
    }
}

using EmployeeManagement.Data.Data;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Shared.ViewModel;
using Microsoft.EntityFrameworkCore;
using BlazorPagination;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Server.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public EmployeeRepository(EmployeeContext employeecontext, IMapper mapper, IConfiguration configuration)
        {
            _employeeContext = employeecontext ??
                throw new ArgumentNullException(nameof(employeecontext));
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<GridParameterViewModel> GetEmployees(int page, int pageSize, string sortColumn, string sortDirection)
        {
            try
            {
                var employee = await _employeeContext.Employees.AsQueryable().OrderBy(sortColumn, sortDirection)
                .ToPagedResultAsync(page, pageSize);
                var temp = new GridParameterViewModel()
            {
                CurrentPage = employee.CurrentPage,
                FirstRowOnPage = employee.FirstRowOnPage,
                LastRowOnPage = employee.LastRowOnPage,
                PageCount = employee.PageCount,
                PageSize = employee.PageSize,
                RowCount = employee.RowCount,
                Results = _mapper.Map<List<EmployeeViewModel>>(employee.Results),
            };
            return temp;
            }
            catch (Exception ex)
            {
                return new GridParameterViewModel();
            }
        }
        public async Task<EmployeeViewModel> GetEmployeeByID(int ID)
        {
            var empResult = await _employeeContext.Employees.FindAsync(ID);
            if (empResult != null)
            {
                var result = _mapper.Map<EmployeeViewModel>(empResult);
                return result;
            }
            return new EmployeeViewModel();
        }
        public async Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel objEmployee)
        {
            var empDetails = new Employee
            {
                EmployeeName = objEmployee.EmployeeName,
                EmailId = objEmployee.EmailId,
                Doj = objEmployee.Doj,
                DepartmentId = objEmployee.DepartmentId                
            };
            var empResult = await _employeeContext.Employees.AddAsync(empDetails);
            await _employeeContext.SaveChangesAsync();
            objEmployee.EmployeeId = empResult.Entity.EmployeeId;
            return objEmployee;
        }
        public async Task<EmployeeViewModel> EditEmployee(EmployeeViewModel objEmployee)
        {
            var empEntity = _mapper.Map<Employee>(objEmployee);
            _employeeContext.Entry(empEntity).State = EntityState.Modified;
            await _employeeContext.SaveChangesAsync();
            return new EmployeeViewModel();
        }
        public async Task<bool> DeleteEmployee(int ID)
        {
            int isEmployeeDeleted;
            var employee = await _employeeContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == ID);
            if (employee != null)
            {
                _employeeContext.Employees.Remove(employee);
                isEmployeeDeleted = await _employeeContext.SaveChangesAsync();
                return (isEmployeeDeleted>0);
            }
            return false;
        }
    }
}
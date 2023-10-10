using AutoMapper;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Shared.ViewModel;

namespace EmployeeManagement.Server
{
    public class MapperClass : Profile
    {
        public MapperClass() 
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}

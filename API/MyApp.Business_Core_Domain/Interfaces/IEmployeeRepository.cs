using MyApp.Business_Core_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business_Core_Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        //Task<IEnumerable<GetRes>> GetEmployeesMapper();
        Task<IEnumerable<EmployeeEntity>> GetEmployeesSP();
        Task<EmployeeEntity> GetEmployeesByIdAsync(Guid id);
        Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity);
        Task<EmployeeEntity> UpdateEmployeeAsync(Guid employeeID, EmployeeEntity entity);
        Task<bool> DeleteEmployeeAsync(Guid employeeID);

        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken);
    }
}

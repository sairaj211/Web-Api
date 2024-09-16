using Microsoft.EntityFrameworkCore;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeesByIdAsync(Guid id)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Employees.Add(entity); 

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<EmployeeEntity> UpdateEmployeeAsync(Guid employeeID, EmployeeEntity entity)
        {
            var employee =  await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeID);

            if(employee != null)
            {
                employee.Name = entity.Name;
                employee.Email = entity.Email;
                employee.Phone = entity.Phone;
                await dbContext.SaveChangesAsync();
                return employee;
            }
            return entity;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid employeeID)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeID);

            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}

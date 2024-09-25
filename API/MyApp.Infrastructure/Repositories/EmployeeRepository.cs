using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IDbConnection dbConnection;

        public EmployeeRepository(AppDbContext _dbContext, IDbConnection _dbConnection)
        {
            dbContext = _dbContext;
            dbConnection = _dbConnection;
        }

        // Entity Framework
        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        // Dapper
        public async Task<IEnumerable<EmployeeEntity>> GetEmployeesSP()
        {
            //var connetcion = new SqlConnection("Server=DESKTOP-UEPM6FT\\SQLEXPRESS;Database=TestAPIDb;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            
            return await dbConnection.QueryAsync<EmployeeEntity>("GetAllEmployees", commandType: CommandType.StoredProcedure);
            
           // return await dbContext.Employees.ToListAsync();
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
                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;
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

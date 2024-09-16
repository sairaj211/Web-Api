using MediatR;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries__For_Read_operations_
{
    public record GetEmployeeByIdQuery(Guid employeeId): IRequest<EmployeeEntity>;
    public class GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<GetEmployeeByIdQuery, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await employeeRepository.GetEmployeesByIdAsync(request.employeeId);
        }
    }

}

using MediatR;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;

namespace MyApp.Application.Queries__For_Read_operations_
{
    public record GetAllEmployeesQuerySP() : IRequest<IEnumerable<EmployeeEntity>>;

    public class GetAllEmployeesQuerySPHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<GetAllEmployeesQuerySP, IEnumerable<EmployeeEntity>>
    {
        public async Task<IEnumerable<EmployeeEntity>> Handle(GetAllEmployeesQuerySP request, CancellationToken cancellationToken)
        {
            return await employeeRepository.GetEmployeesSP();
        }
    }
}

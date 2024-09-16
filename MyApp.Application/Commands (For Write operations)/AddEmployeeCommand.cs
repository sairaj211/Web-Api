using MediatR;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;

namespace MyApp.Application.Commands
{
    public record AddEmployeeCommand(EmployeeEntity Employee) : IRequest<EmployeeEntity>;

    public class AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<AddEmployeeCommand, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.AddEmployeeAsync(request.Employee);
        }
    }
}

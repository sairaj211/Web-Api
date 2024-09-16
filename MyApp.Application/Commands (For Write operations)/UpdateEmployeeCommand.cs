using MediatR;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;

namespace MyApp.Application.Commands__For_Write_operations_
{
    public record UpdateEmployeeCommand(Guid EmployeeId, EmployeeEntity EmployeeEntity) : IRequest<EmployeeEntity>;

    public class UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<UpdateEmployeeCommand, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.UpdateEmployeeAsync(request.EmployeeId, request.EmployeeEntity);
        }
    }
}

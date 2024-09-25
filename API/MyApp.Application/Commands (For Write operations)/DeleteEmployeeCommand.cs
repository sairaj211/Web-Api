using MediatR;
using MyApp.Business_Core_Domain.Interfaces;

namespace MyApp.Application.Commands__For_Write_operations_
{
    public record DeleteEmployeeCommand(Guid EmployeeId) : IRequest<bool>;

    public class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.DeleteEmployeeAsync(request.EmployeeId);
        }
    }
}

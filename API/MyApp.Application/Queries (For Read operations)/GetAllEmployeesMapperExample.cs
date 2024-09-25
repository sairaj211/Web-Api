using AutoMapper;
using MediatR;
using MyApp.Application.Response;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries__For_Read_operations_
{
    public record GetAllEmployeesMapperExample() : IRequest<IEnumerable<GetUserResponse>>;

    public class GetAllEmployeesMapperExampleHandler : IRequestHandler<GetAllEmployeesMapperExample, IEnumerable<GetUserResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesMapperExampleHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUserResponse>> Handle(GetAllEmployeesMapperExample request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployees();
            return _mapper.Map<IEnumerable<GetUserResponse>>(employee);
        }
    }
}
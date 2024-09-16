using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Commands__For_Write_operations_;
using MyApp.Application.Queries__For_Read_operations_;
using MyApp.Business_Core_Domain.Entities;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            var result = await mediator.Send(new AddEmployeeCommand(employee));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var result = await mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }


        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAllEmployeesAsync([FromRoute] Guid employeeId)
        {
            var result = await mediator.Send(new GetEmployeeByIdQuery(employeeId));
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeesAsync([FromRoute] Guid employeeId, [FromBody] EmployeeEntity employeeEntity)
        {
            var result = await mediator.Send(new UpdateEmployeeCommand(employeeId, employeeEntity));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeesAsync([FromRoute] Guid employeeId)
        {
            var result = await mediator.Send(new DeleteEmployeeCommand(employeeId));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}

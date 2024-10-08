﻿using FluentValidation;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<EmployeeEntity> _validator;

        public EmployeeController(IMediator mediator, IValidator<EmployeeEntity> validator)
        {
            _mediator = mediator; 
            _validator = validator;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            var validationResult = await _validator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                // Create a dictionary to hold validation errors
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                return BadRequest(new { errors });
            }

            var result = await _mediator.Send(new AddEmployeeCommand(employee));   
            return Ok(result);
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            //var result = await _mediator.Send(new GetAllEmployeesMapperExample());
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }


        [HttpGet("SP")]
        public async Task<IActionResult> GetAllEmployeesAsyncSP()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuerySP());
            return Ok(result);
        }


        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAllEmployeesAsync([FromRoute] Guid employeeId)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeesAsync([FromRoute] Guid employeeId, [FromBody] EmployeeEntity employeeEntity)
        {
            if(employeeId == Guid.Empty)
            {
                return BadRequest("Employee ID is required");
            }

            employeeEntity.Id = employeeId;

            var validationResult = await _validator.ValidateAsync(employeeEntity);
            if (!validationResult.IsValid)
            {
                // Create a dictionary to hold validation errors
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                return BadRequest(new { errors });
            }

            var result = await _mediator.Send(new UpdateEmployeeCommand(employeeId, employeeEntity));

            return result != null
                ? Ok(new { message = "Employee updated successfully" })
                : NotFound(new { message = "Employee not found" });
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeesAsync([FromRoute] Guid employeeId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(employeeId));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}

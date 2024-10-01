using FluentValidation;
using MyApp.Business_Core_Domain.Entities;
using MyApp.Business_Core_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Validator
{
    public class EmployeeEntityValidator : AbstractValidator<EmployeeEntity>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeEntityValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync((employee, email, cancellationToken) => EmailIsUnique(email, employee.Id, cancellationToken))
                .WithMessage("Email already exists.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 digits long.")
                .MustAsync((employee, phone, cancellationToken) => PhoneIsUnique(phone, employee.Id, cancellationToken))
                .WithMessage("Phone number already exists");
        }

        private async Task<bool> EmailIsUnique(string email, Guid currentEmployeeId, CancellationToken cancellationToken)
        {
            return !await _employeeRepository.EmailExistsAsync(email, currentEmployeeId, cancellationToken);
        }

        private async Task<bool> PhoneIsUnique(string phone, Guid currentEmployeeId, CancellationToken cancellationToken)
        {
            return !await _employeeRepository.PhoneExistsAsync(phone, currentEmployeeId, cancellationToken);
        }
    }
}

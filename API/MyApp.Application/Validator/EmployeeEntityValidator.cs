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
        public EmployeeEntityValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellation) =>
                    !await employeeRepository.EmailExistsAsync(email, cancellation))
                .WithMessage("Email already exists.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 digits long.")
                .MustAsync(async (phone, cancellation) =>
                    !await employeeRepository.PhoneExistsAsync(phone, cancellation))
                .WithMessage("Phone number already exists");
        }
    }
}

using FluentValidation;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Services
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(c => c.HoursWorked).GreaterThan(0).WithMessage("Hours must be > 0");
            RuleFor(c => c.HoursWorked).LessThan(500).WithMessage("Hours cannot exceed 500");
            RuleFor(c => c.HourlyRate).InclusiveBetween(50, 500).WithMessage("Rate R50–R500");
            RuleFor(c => c.TotalAmount).GreaterThan(0).WithMessage("Total must be > 0");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name required");
            RuleFor(c => c.Surname).NotEmpty().WithMessage("Surname required");
            RuleFor(c => c.ContactNumber).Matches(@"^\d{10}$").WithMessage("10-digit phone number");
        }
    }
}
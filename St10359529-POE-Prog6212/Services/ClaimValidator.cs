using FluentValidation;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Services
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(c => c.HoursWorked).GreaterThan(0).WithMessage("Hours must be > 0");
            RuleFor(c => c.HourlyRate).InclusiveBetween(50, 500).WithMessage("Rate must be between 50-500");
            RuleFor(c => c.TotalAmount).GreaterThan(0).WithMessage("Total must be > 0");
        }
    }
}
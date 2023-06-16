using FluentValidation;

namespace MortgageCalculator.SharedLogic.Models
{
    public class ReceivedMortgageDataValidationModel : AbstractValidator<ReceivedMortgageData>
    {
        public ReceivedMortgageDataValidationModel()
        {
            RuleFor(x => x.InterestRate)
                .NotEmpty().WithMessage("interestRate is required, a non zero integer is required");

            RuleFor(x => x.MortgageAmount)
                .NotEmpty().WithMessage("mortgageAmount is required, a non zero integer is required");

            RuleFor(x => x.Term)
                .NotEmpty().WithMessage("term is required, a non zero integer is required");
        }
    }
}

using FluentValidation;

namespace CashFlowControl.Core.Application.Commands
{
    public class ValidateTokenForBearerSchemaCommandValidator : AbstractValidator<ValidateTokenCommand>
    {
        public ValidateTokenForBearerSchemaCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.Scheme).NotEmpty();
        }
    }
}

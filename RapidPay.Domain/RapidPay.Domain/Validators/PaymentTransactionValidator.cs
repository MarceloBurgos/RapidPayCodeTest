using FluentValidation;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Repositories;
using RapidPay.Domain.Resources;

namespace RapidPay.Domain.Validators;

public class PaymentTransactionValidator : AbstractValidator<PaymentTransaction>
{
	public PaymentTransactionValidator(IGenericRepository<Guid> genericRepository)
	{
		RuleFor(x => x.Card)
			.NotNull()
			.WithMessage(ValidationMessages.RP004);

		RuleFor(x => x.Amount)
			.GreaterThan(0)
			.WithMessage(ValidationMessages.RP006);
	}
}

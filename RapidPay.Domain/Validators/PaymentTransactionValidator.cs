using FluentValidation;
using FluentValidation.Results;
using RapidPay.Domain.CustomExceptions;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Repositories;
using RapidPay.Domain.Resources;

namespace RapidPay.Domain.Validators;

public class PaymentTransactionValidator : AbstractValidator<PaymentTransaction>
{
	/// <summary>
	/// Creates a valid <see cref="PaymentTransactionValidator"/> instance.
	/// </summary>
	public PaymentTransactionValidator(IGenericRepository<Guid> genericRepository, long cardNumber)
	{
		_cardNumber = cardNumber;

		RuleFor(x => x.Card)
			.NotNull()
			.WithErrorCode(nameof(ValidationMessages.RP004))
			.WithMessage(ValidationMessages.RP004);

		RuleFor(x => x.Amount)
			.GreaterThan(0)
			.WithErrorCode(nameof(ValidationMessages.RP005))
			.WithMessage(ValidationMessages.RP005);
	}

	/// <inheritdoc />
	protected override void RaiseValidationException(ValidationContext<PaymentTransaction> context, ValidationResult result)
	{
		var paymentTransactionException = new PaymentTransactionException(_cardNumber);
		result.Errors.ForEach(error =>
		{
			if (!paymentTransactionException.Errors.Any(x => x.Code.Equals(error.ErrorCode)))
			{
				paymentTransactionException.Errors.Add((error.ErrorCode, error.ErrorMessage));
			}
		});

		throw paymentTransactionException;
	}

	private readonly long _cardNumber;
}

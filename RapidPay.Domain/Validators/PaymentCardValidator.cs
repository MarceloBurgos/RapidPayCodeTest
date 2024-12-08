using FluentValidation;
using FluentValidation.Results;
using RapidPay.Domain.CustomExceptions;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Repositories;
using RapidPay.Domain.Resources;

namespace RapidPay.Domain.Validators;

public class PaymentCardValidator : AbstractValidator<PaymentCard>
{
	/// <summary>
	/// Creates a valid <see cref="PaymentCardValidator"/> instance.
	/// </summary>
	public PaymentCardValidator(IGenericRepository<Guid> genericRepository)
	{
		RuleLevelCascadeMode = CascadeMode.Stop;

		RuleFor(x => x.Number)
			.InclusiveBetween(100000000000000, 999999999999999)
			.WithErrorCode(nameof(ValidationMessages.RP001))
			.WithMessage(ValidationMessages.RP001)
			.MustAsync(async (paymentCard, cardNumber, _) =>
			{
				return await genericRepository.GetBy<PaymentCard>(x => x.Number == cardNumber && x.Id != paymentCard.Id) is not { };
			})
			.WithErrorCode(nameof(ValidationMessages.RP002))
			.WithMessage(ValidationMessages.RP002);
	}

	/// <inheritdoc />
	protected override void RaiseValidationException(ValidationContext<PaymentCard> context, ValidationResult result)
	{
		var entityException = new EntityValidationException<PaymentCard>(context.InstanceToValidate);
		result.Errors.ForEach(error =>
		{
			if (!entityException.Errors.Any(x => x.Code.Equals(error.ErrorCode)))
			{
				entityException.Errors.Add((error.ErrorCode, error.ErrorMessage));
			}
		});

		throw entityException;
	}
}

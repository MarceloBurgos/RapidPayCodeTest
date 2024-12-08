using FluentValidation;
using RapidPay.Domain.CustomExceptions;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Repositories;
using RapidPay.Domain.Validators;

namespace RapidPay.Application.CardManagement;

public class CardManagementService(IGenericRepository<Guid> genericRepository)
{
	public async Task<Guid> RegisterPaymentCard(long cardNumber)
	{
		var paymentCard = new PaymentCard(cardNumber);

		var paymentCardValidator = new PaymentCardValidator(genericRepository);
		await paymentCardValidator.ValidateAndThrowAsync(paymentCard);

		await genericRepository.Save(paymentCard);

		return paymentCard.Id;
	}

	public async Task<decimal> GetBalance(long cardNumber)
	{
		var paymentCard = await genericRepository.GetBy<PaymentCard>(x => x.Number == cardNumber);

		if (paymentCard is not { })
		{
			throw new PaymentCardNotFoundException(cardNumber);
		}

		return paymentCard.Balance;
	}
}

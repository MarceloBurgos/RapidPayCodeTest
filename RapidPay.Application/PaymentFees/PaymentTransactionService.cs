using FluentValidation;
using RapidPay.Domain.Entities;
using RapidPay.Domain.ExternalServices;
using RapidPay.Domain.Repositories;
using RapidPay.Domain.Validators;

namespace RapidPay.Application.PaymentFees;

public class PaymentTransactionService(IGenericRepository<Guid> genericRepository, IUniversalFeesExchangeProvider ufeProvider)
{
    public async Task Pay(long cardNumber, decimal amount)
    {
        var paymentCard = await genericRepository.GetBy<PaymentCard>(x => x.Number == cardNumber);
        var paymentTransaction = new PaymentTransaction(amount, ufeProvider.NextFee(), paymentCard!);

        var paymentTransactionValidator = new PaymentTransactionValidator(genericRepository, cardNumber);
        await paymentTransactionValidator.ValidateAndThrowAsync(paymentTransaction);
        await genericRepository.Save(paymentTransaction);

        paymentCard!.Balance += paymentTransaction.TotalPayment;
        await genericRepository.Save(paymentCard);
    }

    public async Task<ICollection<PaymentTransaction>> ListPayments(long? cardNumber)
    {
        return cardNumber.HasValue
            ? await genericRepository.ListBy<PaymentTransaction>(x => x.Card.Number == cardNumber)
            : await genericRepository.ListAll<PaymentTransaction>();
    }
}

using FluentAssertions;
using RapidPay.Application.CardManagement;
using RapidPay.Application.PaymentFees;
using RapidPay.Domain.CustomExceptions;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Resources;
using Xunit.Abstractions;

namespace RapidPay.UnitTests.Services;

public class PaymentTransactionTests : RapidPayServiceBase
{
	public PaymentTransactionTests(ITestOutputHelper output, ContextSetUpInitializerFixture contextSetUpInitializerFixture) : base(output, contextSetUpInitializerFixture)
	{
		_cardManagementService = new CardManagementService(GenericRepository);
		_paymentTransactionService = new PaymentTransactionService(GenericRepository, CustomTimeFeeExchangeProvider);

		_cardManagementService.RegisterPaymentCard(VALID_CARD_NUMBER).Wait();
	}

	[Fact]
	public async Task GetBalance_WhenCreditCardNotExists_ShouldThrowException()
	{
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 200.66m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 300.77m);

		var payments = await GenericRepository.ListAll<PaymentTransaction>();
		var balance = await _cardManagementService.GetBalance(VALID_CARD_NUMBER);

		var exception = await Assert.ThrowsAsync<PaymentCardNotFoundException>(async () => await _cardManagementService.GetBalance(100000000000001));

		Assert.NotNull(exception);
		exception.Message.Should().Contain($"{nameof(ValidationMessages.RP004)} {ValidationMessages.RP004}. Card: {100000000000001}");
	}

	[Fact]
	public async Task GetBalance_WhenManyPaymentsHaveDifferentFee_ShouldReturnTotalBalance()
	{
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 200.66m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 300.77m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 400.88m);

		Thread.Sleep(TimeSpan.FromSeconds(5));

		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 500.99m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 600.11m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 700.22m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 800.33m);

		var payments = await GenericRepository.ListAll<PaymentTransaction>();
		var balance = await _cardManagementService.GetBalance(VALID_CARD_NUMBER);
		balance.Should().Be(payments.Sum(payment => payment.TotalPayment));

		foreach (var payment in payments)
		{
			Output.WriteLine(payment.ToString());
		}
	}

	[Fact]
	public async Task GetBalance_WhenManyPaymentsHaveSameFee_ShouldReturnTotalBalance()
	{
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 200.66m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 300.77m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 400.88m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 500.99m);

		var payments = await GenericRepository.ListAll<PaymentTransaction>();
		var balance = await _cardManagementService.GetBalance(VALID_CARD_NUMBER);
		balance.Should().Be(payments.Sum(payment => payment.TotalPayment));

		foreach (var payment in payments)
		{
			Output.WriteLine(payment.ToString());
		}
	}

	[Fact]
	public async Task Pay_WhenFeeGenerationKeepsEqualBetweenPaymentTransactions_ShouldSavePayments()
	{
		var fee = CustomTimeFeeExchangeProvider.NextFee();
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 200.66m);
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 300.77m);

		var payments = await GenericRepository.ListAll<PaymentTransaction>();

		Assert.NotEmpty(payments);
		payments.Should().HaveCount(3);
		Assert.Collection(payments,
			transaction =>
			{
				transaction.Fee.Should().Be(fee);
				transaction.Amount.Should().Be(100.55m);
				Output.WriteLine($"Fee {fee}");
			},
			transaction =>
			{
				transaction.Fee.Should().Be(fee);
				transaction.Amount.Should().Be(200.66m);
				Output.WriteLine($"Fee {fee}");
			},
			transaction =>
			{
				transaction.Fee.Should().Be(fee);
				transaction.Amount.Should().Be(300.77m);
				Output.WriteLine($"Fee {fee}");
			});
	}

	[Fact]
	public async Task Pay_WhenFeeGenerationChangesBetweenPaymentTransactions_ShouldSavePayments()
	{
		var firstFee = CustomTimeFeeExchangeProvider.NextFee();
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);
		
		Thread.Sleep(TimeSpan.FromSeconds(5));
		var secondFee = CustomTimeFeeExchangeProvider.NextFee();
		await _paymentTransactionService.Pay(VALID_CARD_NUMBER, 100.55m);

		var payments = await GenericRepository.ListAll<PaymentTransaction>();

		firstFee.Should().NotBe(secondFee);
		Assert.NotEmpty(payments);
		payments.Should().HaveCount(2);
		Assert.Collection(payments,
			transaction =>
			{
				transaction.Fee.Should().Be(firstFee);
				transaction.Amount.Should().Be(100.55m);
				Output.WriteLine($"Fee {firstFee}");
			},
			transaction =>
			{
				transaction.Fee.Should().Be(secondFee);
				transaction.Amount.Should().Be(100.55m);
				Output.WriteLine($"Fee {secondFee}");
			});
	}

	[Theory]
	[InlineData(0.00)]
	[InlineData(-0.01)]
	public async Task Pay_WhenAmountHasInvalidValues_ShouldThrowException(decimal amount)
	{
		var exception = await Assert.ThrowsAsync<PaymentTransactionException>(async () => await _paymentTransactionService.Pay(VALID_CARD_NUMBER, amount));

		Assert.NotNull(exception);
		exception.CardNumber.Should().Be(VALID_CARD_NUMBER);
		exception.Errors.Should().HaveCount(1);
		Assert.Single(exception.Errors,
			item =>
			{
				item.Code.Should().Be(nameof(ValidationMessages.RP005));
				item.Description.Should().Be(ValidationMessages.RP005);
				return true;
			});
	}

	[Fact]
	public async Task Pay_WhenCardNumberDoesNotExist_ShouldThrowException()
	{
		var exception = await Assert.ThrowsAsync<PaymentTransactionException>(async () => await _paymentTransactionService.Pay(100000000000001, 100.50m));

		Assert.NotNull(exception);
		exception.CardNumber.Should().Be(100000000000001);
		exception.Errors.Should().HaveCount(1);
		Assert.Single(exception.Errors,
			item =>
			{
				item.Code.Should().Be(nameof(ValidationMessages.RP004));
				item.Description.Should().Be($"{ValidationMessages.RP004}. Card: {100000000000001}");
				return true;
			});
	}

	private const long VALID_CARD_NUMBER = 100000000000000;
	private readonly PaymentTransactionService _paymentTransactionService;
	private readonly CardManagementService _cardManagementService;
}

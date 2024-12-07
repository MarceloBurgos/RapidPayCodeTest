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
		//_paymentTransactionService = new PaymentTransactionService(GenericRepository);
	}

	private readonly PaymentTransactionService _paymentTransactionService;
}

public class CardManagementTests : RapidPayServiceBase
{
	public CardManagementTests(ITestOutputHelper output, ContextSetUpInitializerFixture contextSetUpInitializerFixture) : base(output, contextSetUpInitializerFixture)
	{
		_cardManagementService = new CardManagementService(GenericRepository);
	}

	[Fact]
	public async Task RegisterPaymentCard_WhenNumberIsValid_ShouldSaveNewCard()
	{
		var paymentCardId = await _cardManagementService.RegisterPaymentCard(100000000000001);
		paymentCardId.Should().NotBeEmpty();
	}

	[Fact]
	public async Task RegisterPaymentCard_WhenNumberIsDuplicate_ShouldThrowException()
	{
		await _cardManagementService.RegisterPaymentCard(100000000000000);

		var exception = await Assert.ThrowsAsync<EntityValidationException<PaymentCard>>(async () => await _cardManagementService.RegisterPaymentCard(100000000000000));

		Assert.NotNull(exception);
		exception.Errors.Should().HaveCount(1);
		Assert.Single(exception.Errors,
			item =>
			{
				item.Code.Should().Be(nameof(ValidationMessages.RP002));
				item.Description.Should().Be(ValidationMessages.RP002);
				return true;
			});
	}

	[Theory]
	[InlineData(99999999999999)]
	[InlineData(1000000000000000)]
	[InlineData(-100000000000000)]
	public async Task RegisterPaymentCard_WhenNumberNotMatch15Digits_ShouldThrowException(long cardNumber)
	{
		var exception = await Assert.ThrowsAsync<EntityValidationException<PaymentCard>>(async () => await _cardManagementService.RegisterPaymentCard(cardNumber));

		Assert.NotNull(exception);
		exception.Errors.Should().HaveCount(1);
		Assert.Single(exception.Errors,
			item =>
			{
				item.Code.Should().Be(nameof(ValidationMessages.RP001));
				item.Description.Should().Be(ValidationMessages.RP001);
				return true;
			});
	}

	private readonly CardManagementService _cardManagementService;
}

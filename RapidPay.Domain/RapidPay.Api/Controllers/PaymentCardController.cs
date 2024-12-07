using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.Controllers.Base;
using RapidPay.Application.CardManagement;
using RapidPay.Domain.Entities;

namespace RapidPay.Api.Controllers;

public class PaymentCardController(CardManagementService cardManagementService) : RapidPayBaseController
{
	[HttpPost]
	public async Task<IActionResult> RegisterPaymentCard(long cardNumber)
	{
		var paymentCardId = await cardManagementService.RegisterPaymentCard(cardNumber);
		return Created($"/{nameof(PaymentCard)}/{paymentCardId}", paymentCardId);
	}

	[HttpGet]
	public async Task<IActionResult> GetBalance(long cardNumber)
	{
		var balance = await cardManagementService.GetBalance(cardNumber);
		return Ok(balance);
	}
}

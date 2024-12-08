using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.Controllers.Base;
using RapidPay.Application.PaymentFees;

namespace RapidPay.Api.Controllers;

public class PaymentTransactionController(PaymentTransactionService paymentTransactionService) : RapidPayBaseController
{
	[HttpPost]
	public async Task<IActionResult> PayTransaction(long cardNumber, decimal amount)
	{
		await paymentTransactionService.Pay(cardNumber, amount);
		return Ok();
	}
}

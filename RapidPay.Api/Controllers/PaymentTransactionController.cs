using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.AutoMapper.Models;
using RapidPay.Api.Controllers.Base;
using RapidPay.Application.PaymentFees;
using System.ComponentModel.DataAnnotations;

namespace RapidPay.Api.Controllers;

public class PaymentTransactionController(PaymentTransactionService paymentTransactionService, IMapper mapper) : RapidPayBaseController(mapper)
{
	[HttpPost]
	public async Task<IActionResult> PayTransaction([Required] long cardNumber, [Required] decimal amount)
	{
		await paymentTransactionService.Pay(cardNumber, amount);
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> ListPayments(long? cardNumber)
	{
		var payments = await paymentTransactionService.ListPayments(cardNumber);
		return Ok(Mapper.Map<ICollection<PaymentTransactionModel>>(payments));
	}
}

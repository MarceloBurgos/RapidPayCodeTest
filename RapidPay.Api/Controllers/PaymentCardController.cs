using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.Controllers.Base;
using RapidPay.Application.CardManagement;
using RapidPay.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RapidPay.Api.Controllers;

public class PaymentCardController(CardManagementService cardManagementService, IMapper mapper) : RapidPayBaseController(mapper)
{
	[HttpPost]
	public async Task<IActionResult> RegisterPaymentCard([Required] long cardNumber)
	{
		var paymentCardId = await cardManagementService.RegisterPaymentCard(cardNumber);
		return Created($"/{nameof(PaymentCard)}/{paymentCardId}", paymentCardId);
	}

	[HttpGet]
	public async Task<IActionResult> GetBalance([Required] long cardNumber)
	{
		var balance = await cardManagementService.GetBalance(cardNumber);
		return Ok(balance);
	}
}

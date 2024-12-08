using AutoMapper;
using RapidPay.Api.AutoMapper.Models;
using RapidPay.Domain.Entities;

namespace RapidPay.Api.AutoMapper.Profiles;

public class PaymentTransactionModelProfile : Profile
{
	public PaymentTransactionModelProfile()
	{
		CreateMap<PaymentTransaction, PaymentTransactionModel>()
			.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.Card.Number));
	}
}

namespace RapidPay.Api.AutoMapper.Models;

public record PaymentTransactionModel(DateTime PaymentDate, decimal Amount, decimal Fee, long CardNumber);

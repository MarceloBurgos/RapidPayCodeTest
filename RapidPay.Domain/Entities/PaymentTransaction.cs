namespace RapidPay.Domain.Entities;

public class PaymentTransaction
{
    protected PaymentTransaction()
    {
    }

    public PaymentTransaction(decimal amount, decimal fee, PaymentCard card)
    {
        Amount = amount;
        Fee = fee;
        Card = card;
    }

    public Guid Id { get; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public decimal Amount { get; set; }

    public decimal Fee { get; set; }

    public decimal TotalPayment => Amount + Fee;

    public virtual PaymentCard Card { get; set; }

    public override string ToString() => $"Card number {Card?.Number} Amount {Amount:0.000} Fee {Fee:0.0000}";
}

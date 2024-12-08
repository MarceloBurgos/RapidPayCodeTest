namespace RapidPay.Domain.Entities;

public class PaymentCard
{
    protected PaymentCard()
    {
    }

    public PaymentCard(long number)
    {
        Number = number;
    }

    public Guid Id { get; }

    public long Number { get; set; }

    public decimal Balance { get; set; } = decimal.Zero;
}

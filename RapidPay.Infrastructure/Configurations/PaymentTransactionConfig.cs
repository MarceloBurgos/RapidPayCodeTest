using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Configurations;

public class PaymentTransactionConfig : IEntityTypeConfiguration<PaymentTransaction>
{
	public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
	{
		builder.ToTable("PaymentTransactions");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>().ValueGeneratedOnAdd();
		builder.Property(x => x.PaymentDate).IsRequired();
		builder.Property(x => x.Amount).HasPrecision(19, 5).IsRequired();
		builder.Property(x => x.Fee).HasPrecision(19, 5).IsRequired();
		builder.HasOne(x => x.Card).WithMany().HasForeignKey("PaymentCardId").IsRequired();
		builder.Ignore(x => x.TotalPayment);
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RapidPay.Domain;
using RapidPay.Domain.Entities;
using System.Dynamic;

namespace RapidPay.Infrastructure.Configurations;

public class PaymentCardConfig : IEntityTypeConfiguration<PaymentCard>
{
	public void Configure(EntityTypeBuilder<PaymentCard> builder)
	{
		builder.ToTable("PaymentCards");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>().ValueGeneratedOnAdd();
		builder.Property(x => x.Number).HasMaxLength(15).IsRequired();
		builder.Property(x => x.Balance).HasPrecision(19, 5).IsRequired();
	}
}

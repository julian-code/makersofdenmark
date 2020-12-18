using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MakersOfDenmark.Infrastructure.Persistence.EntityConfigurations
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.HasMany(b => b.Users)
                .WithMany(u => u.Badges)
                .UsingEntity(j => j.ToTable("BadgeAssignedToUser"));
        }
    }
}

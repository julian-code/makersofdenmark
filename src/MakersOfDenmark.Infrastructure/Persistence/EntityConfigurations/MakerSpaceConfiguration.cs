using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Infrastructure.Persistence.EntityConfigurations
{
    public class MakerSpaceConfiguration : IEntityTypeConfiguration<MakerSpace>
    {
        public void Configure(EntityTypeBuilder<MakerSpace> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Members)
                .WithMany(m => m.MakerSpaces)
                .UsingEntity(j => j.ToTable("MakerSpaceHasMembers"));

            builder.HasMany(x => x.Admins)
                .WithMany(u=>u.AdminOn)
                .UsingEntity(j => j.ToTable("MakerSpaceHasAdmins"));

            builder.HasMany(x => x.Tools)
                .WithMany(t => t.MakerSpaces)
                .UsingEntity(j => j.ToTable("MakerSpaceHasTools"));
            builder.HasMany(x => x.Events)
                .WithOne(e => e.MakerSpace)
                .HasForeignKey(e => e.MakerSpaceId);
            builder.HasMany(m => m.Badges)
                .WithMany(b => b.MakerSpaces)
                .UsingEntity(j => j.ToTable("MakerSpaceHasBadges"));
        }
    }
}

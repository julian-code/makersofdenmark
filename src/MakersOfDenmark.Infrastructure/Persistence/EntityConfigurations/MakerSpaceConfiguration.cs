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

        }
    }
}

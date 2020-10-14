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
        }
    }
}

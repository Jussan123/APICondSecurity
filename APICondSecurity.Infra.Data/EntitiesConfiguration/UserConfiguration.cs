

using APICondSecurity.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APICondSecurity.Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.id_user);
            builder.Property(x => x.name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.email).HasMaxLength(250).IsRequired();
            builder.Property(x => x.telefone).HasMaxLength(255).IsRequired();
            builder.Property(x => x.situacao).HasMaxLength(2).IsRequired();
        }
    }
}


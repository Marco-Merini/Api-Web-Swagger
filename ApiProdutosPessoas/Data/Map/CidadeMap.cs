using ApiProdutosPessoas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Data.Map
{
    public class CidadeMap : IEntityTypeConfiguration<CidadeModel>
    {
        public void Configure(EntityTypeBuilder<CidadeModel> builder)
        {
            builder.HasKey(x => x.codigoIBGE);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.UF).IsRequired();
            builder.Property(x => x.CodigoPais).IsRequired();
        }
    }
}

using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.WebApi.Domain.ATS;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class CustomerConfig : IEntityTypeConfiguration<CustomerModel>
{
    public void Configure(EntityTypeBuilder<CustomerModel> builder)
    {

        builder
            .Property(b => b.QRRefference)
                .HasMaxLength(256);
    }
}

public class CustomerConfig : IEntityTypeConfiguration<CustomerProductModel>
{
    public void Configure(EntityTypeBuilder<CustomerProductModel> builder)
    {

        builder
            .Property(b => b.ProductName)
                .HasMaxLength(256);
    }
}

public class NotesConfig : IEntityTypeConfiguration<NotesModel>
{
    public void Configure(EntityTypeBuilder<NotesModel> builder)
    {
        builder
            .Property(b => b.NoteTitle)
                .HasMaxLength(256);

    }
}
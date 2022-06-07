using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProjectManagement.DataAccess.Configurations
{
    public class DeveloperPhoneNumberConfiguration : IEntityTypeConfiguration<DeveloperPhoneNumber>
    {
        public void Configure(EntityTypeBuilder<DeveloperPhoneNumber> builder)
        {
            builder.Property(d => d.Id).HasDefaultValueSql("(newid())");
            builder.Property(d => d.Number).IsRequired();

            builder.HasOne(d => d.Developer)
                .WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.DeveloperId)
                .HasConstraintName("FK_DeveloperPhoneNumber_Developer_DeveloperId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(GetAllDeveloperPhoneNumber());
        }

        private IEnumerable<DeveloperPhoneNumber> GetAllDeveloperPhoneNumber()
        {
            yield return new DeveloperPhoneNumber() { Id = Guid.Parse("F48FBFB5-1E58-4937-85DD-F7A8E853F605"), Number = "+51 123-45-67", DeveloperId = new Guid("b0287537-dfe1-4f44-8666-5da587651bf7") };
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProjectManagement.DataAccess.Configurations
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        #region Methods
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.Property(d => d.Id).HasDefaultValueSql("(newid())");
            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(255);

            builder.HasOne(d => d.FavoriteLanguage)
                .WithOne()
                .HasForeignKey<Developer>(d => d.ProgrammingLanguageId)
                .HasConstraintName("FK_Developer_ProgrammingLanguage_ProgrammingLanguageId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(GetAllDeveloper());
        }

        public IEnumerable<Developer> GetAllDeveloper()
        {
            yield return new Developer() { Id = Guid.Parse("b0287537-dfe1-4f44-8666-5da587651bf7"), FirstName = "Larry", LastName = "Page" };
            yield return new Developer() { Id = Guid.Parse("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"), FirstName = "Bill", LastName = "Gates" };
            yield return new Developer() { Id = Guid.Parse("fb1459c7-393f-45b6-a662-dce000d9aac0"), FirstName = "Mark", LastName = "Zuckerberg" };
            yield return new Developer() { Id = Guid.Parse("08d38390-b579-40bc-8396-e355d52bc823"), FirstName = "Ken", LastName = "Thompson" };
            yield return new Developer() { Id = Guid.Parse("3aaaa471-54c9-4162-844d-9accf7a6ad80"), FirstName = "Linus", LastName = "Torvalds" };
            yield return new Developer() { Id = Guid.Parse("1ea74fc5-e844-4f70-b37f-ac2c9d6cb43c"), FirstName = "Ada", LastName = "Lovelace" };
            yield return new Developer() { Id = Guid.Parse("0bcd05a8-f538-48f1-a1d2-382f5e949c11"), FirstName = "Alan", LastName = "Turing" };
        }
        #endregion
    }
}
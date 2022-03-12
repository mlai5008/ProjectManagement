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

            builder.HasData(GetAllDeveloper());
        }

        public IEnumerable<Developer> GetAllDeveloper()
        {
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Larry", LastName = "Page" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Bill", LastName = "Gates" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Mark", LastName = "Zuckerberg" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Ken", LastName = "Thompson" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Linus", LastName = "Torvalds" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Ada", LastName = "Lovelace" };
            yield return new Developer() { Id = Guid.NewGuid(), FirstName = "Alan", LastName = "Turing" };
        }
        #endregion
    }
}
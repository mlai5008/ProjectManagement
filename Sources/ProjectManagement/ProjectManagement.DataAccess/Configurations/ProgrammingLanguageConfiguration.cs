using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProjectManagement.DataAccess.Configurations
{
    public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
        {
            builder.Property(d => d.Id).HasDefaultValueSql("(newid())");
            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);

            builder.HasData(GetAllProgrammingLanguage());
        }

        public IEnumerable<ProgrammingLanguage> GetAllProgrammingLanguage()
        {
            yield return new ProgrammingLanguage { Id = Guid.Parse("E075DF46-8637-4395-866A-8E322BA6663C"), Name = "Python" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("2FCF3A18-54DD-4BB2-99A7-EDC79052585A"), Name = "Java" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("0065A6C7-4140-4CC1-B0EC-71BA82894FE3"), Name = "Ruby" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("749856EB-7F8A-47E5-BD1C-F6AB09502D99"), Name = "C#" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("96118808-4BCA-4299-9195-F655C019E5AC"), Name = "C++" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("18FD93EB-1CC6-4297-8CF5-92730E25CD81"), Name = "JavaScript" };
            yield return new ProgrammingLanguage { Id = Guid.Parse("8697765D-FF50-415C-866F-696B23F6CEA5"), Name = "Go" };
        }
    }
}
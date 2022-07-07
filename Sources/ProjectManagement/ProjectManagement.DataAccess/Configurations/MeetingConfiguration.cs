using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProjectManagement.DataAccess.Configurations
{
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.Property(d => d.Id).HasDefaultValueSql("(newid())");
            builder.Property(d => d.Title).IsRequired().HasMaxLength(50);

            builder.HasMany(m => m.Developers)
                .WithMany(d => d.Meetings)
                .UsingEntity(dm => dm.ToTable("DeveloperMeeting").HasData(
                new { DevelopersId = Guid.Parse("b0287537-dfe1-4f44-8666-5da587651bf7"), MeetingsId = Guid.Parse("2EF776FE-0B95-4F13-B299-B49B81EB33DB") },
                new { DevelopersId = Guid.Parse("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"), MeetingsId = Guid.Parse("2EF776FE-0B95-4F13-B299-B49B81EB33DB") }));
            
            builder.HasData(GetAllMeeting());
        }

        private IEnumerable<Meeting> GetAllMeeting()
        {
            Developer developerFirst = new Developer() { Id = Guid.Parse("b0287537-dfe1-4f44-8666-5da587651bf7"), FirstName = "Larry", LastName = "Page" };
            Developer developerSeconds = new Developer() { Id = Guid.Parse("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"), FirstName = "Bill", LastName = "Gates" };
            Meeting meeting = new Meeting() { Id = Guid.Parse("2EF776FE-0B95-4F13-B299-B49B81EB33DB"), Title = "Watching Soccer", DateTo = DateTime.Now.AddDays(-2), DateFrom = DateTime.Now.AddDays(-1) };
            developerFirst.Meetings.Add(meeting);
            developerSeconds.Meetings.Add(meeting);
            return new List<Meeting>() { meeting };
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Configurations;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Services.Interfaces;

namespace ProjectManagement.DataAccess.Context
{
    public sealed class ProjectManagementDbContext : DbContext
    {
        #region Field
        private readonly IConfigSettingsService _configSettingsService;
        #endregion

        #region Ctor
        public ProjectManagementDbContext(IConfigSettingsService configSettingsService)
        {
            _configSettingsService = configSettingsService;
            Database.Migrate();
        }

        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options)
        { } 
        #endregion

        #region Properties
        public DbSet<Developer> Developers { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configSettingsService.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
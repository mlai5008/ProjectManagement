using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Configurations;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Services.Interfaces;

namespace ProjectManagement.DataAccess.Context
{
    public class ProjectManagementDbContext : DbContext
    {
        #region Field
        private readonly IConfigSettingsService _configSettingsService;
        #endregion

        #region Ctor
        public ProjectManagementDbContext() { }

        public ProjectManagementDbContext(IConfigSettingsService configSettingsService)
        {
            _configSettingsService = configSettingsService;
            Database.Migrate();
        }

        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options)
        { } 
        #endregion

        #region Properties
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public virtual DbSet<DeveloperPhoneNumber> DeveloperPhoneNumbers { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configSettingsService.GetConnectionString());

                #region For MIGRATION
                //TODO: only for migration
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectManagement;Integrated Security=True");
                #endregion
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration())
                .ApplyConfiguration(new ProgrammingLanguageConfiguration())
                .ApplyConfiguration(new DeveloperPhoneNumberConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
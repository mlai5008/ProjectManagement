using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Configurations;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.DataAccess.Context
{
    public sealed class ProjectManagementDbContext : DbContext
    {
        #region Field
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectManagement;Integrated Security=True"; 
        #endregion

        #region Ctor
        public ProjectManagementDbContext()
        {
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
                optionsBuilder.UseSqlServer(ConnectionString);
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